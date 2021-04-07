﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.Options;
using Consul;
using Microsoft.Extensions.Configuration;

namespace CodeGenerator.Infra.Common.Configurations
{
    public sealed class DefaultConsulConfigurationProvider : ConfigurationProvider
    {
        private readonly ConsulClient _consulClient;
        private readonly string _path;
        private readonly int _waitMillisecond;
        private readonly bool _reloadOnChange;
        private ulong _currentIndex;
        private Task _pollTask;

        public DefaultConsulConfigurationProvider(ConsulOptions consulOption, bool reloadOnChanges)
        {
            _consulClient = new ConsulClient(x =>
            {
                // consul 服务地址
                x.Address = new Uri(consulOption.ConsulUrl);
            });
            _path = consulOption.ConsulKeyPath;
            _waitMillisecond = 3;
            _reloadOnChange = reloadOnChanges;
        }

        public override void Load()
        {
            if (_pollTask != null)
            {
                return;
            }
            //加载数据
            LoadData(GetData().GetAwaiter().GetResult());
            //处理数据变更
            PollReload();
        }

        //设置数据
        private void LoadData(QueryResult<KVPair> queryResult)
        {
            _currentIndex = queryResult.LastIndex;
            if (queryResult.Response == null
                || queryResult.Response.Value == null
                || !queryResult.Response.Value.Any())
            {
                return;
            }
            Stream stream = new MemoryStream(queryResult.Response.Value);
            Data = JsonConfigurationFileParser.Parse(stream);
        }

        //获取consul配置中心数据
        private async Task<QueryResult<KVPair>> GetData()
        {
            var res = await _consulClient.KV.Get(_path);
            if (res.StatusCode == HttpStatusCode.OK || res.StatusCode == HttpStatusCode.NotFound)
            {
                return res;
            }
            throw new Exception($"Error loading configuration from consul. Status code: {res.StatusCode}.");
        }

        //处理数据变更
        private void PollReload()
        {
            if (_reloadOnChange)
            {
                _pollTask = Task.Run(async () =>
                {
                    while (true)
                    {
                        QueryResult<KVPair> queryResult = await GetData();
                        if (queryResult.LastIndex != _currentIndex)
                        {
                            LoadData(queryResult);
                            OnReload();
                        }
                        await Task.Delay(_waitMillisecond);
                    }
                });
            }
        }
    }
}
