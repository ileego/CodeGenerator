alter table application_action 
   drop foreign key FK_APPLICAT_APPLICATI_APPLICAT;

alter table application_menu 
   drop foreign key FK_APPLICAT_MENU_APPL_APPLICAT;

alter table application_menu 
   drop foreign key FK_APPLICAT_PARENT_ME_APPLICAT;

alter table application_menu_action 
   drop foreign key FK_APPLICAT_APPLICATI_APPLICAT;

alter table application_menu_action 
   drop foreign key FK_APPLICAT_APPLICATI_APPLICAT;

alter table application_role 
   drop foreign key FK_APPLICAT_ROLE_APPL_APPLICAT;

alter table available_action 
   drop foreign key FK_AVAILABL_AVAILABLE_APPLICAT;

alter table available_action 
   drop foreign key FK_AVAILABL_AVAILABLE_ROLE_AUT;

alter table clients_authorize 
   drop foreign key FK_CLIENTS__CLIENT_AU_API_RESO;

alter table clients_authorize 
   drop foreign key FK_CLIENTS__CLIENT_AU_CLIENT;

alter table data_access_authorize 
   drop foreign key FK_DATA_ACC_DATA_ACCE_DISTRICT;

alter table data_access_authorize 
   drop foreign key FK_DATA_ACC_DATA_ACCE_APPLICAT;

alter table department 
   drop foreign key FK_DEPARTME_DEPARTMEN_ORGANIZA;

alter table department 
   drop foreign key FK_DEPARTME_PARENT_DE_DEPARTME;

alter table employee 
   drop foreign key FK_EMPLOYEE_EMPLOYEE__DEPARTME;

alter table employee_authorize 
   drop foreign key FK_EMPLOYEE_EMPLOYEE__EMPLOYEE;

alter table employee_authorize 
   drop foreign key FK_EMPLOYEE_EMPLOYEE__APPLICAT;

alter table organization 
   drop foreign key FK_ORGANIZA_ORGANIZAT_DISTRICT;

alter table organization 
   drop foreign key FK_ORGANIZA_ORGANIZAT_ORGANIZA;

alter table organization 
   drop foreign key FK_ORGANIZA_PARENT_OR_ORGANIZA;

alter table role_authorize 
   drop foreign key FK_ROLE_AUT_ROLE_AUTH_APPLICAT;

alter table role_authorize 
   drop foreign key FK_ROLE_AUT_ROLE_AUTH_APPLICAT;

drop table if exists api_resource;


alter table application_action 
   drop foreign key FK_APPLICAT_APPLICATI_APPLICAT;

drop table if exists application_action;


alter table application_menu 
   drop foreign key FK_APPLICAT_MENU_APPL_APPLICAT;

alter table application_menu 
   drop foreign key FK_APPLICAT_PARENT_ME_APPLICAT;

drop table if exists application_menu;


alter table application_menu_action 
   drop foreign key FK_APPLICAT_APPLICATI_APPLICAT;

alter table application_menu_action 
   drop foreign key FK_APPLICAT_APPLICATI_APPLICAT;

drop table if exists application_menu_action;


alter table application_role 
   drop foreign key FK_APPLICAT_ROLE_APPL_APPLICAT;

drop table if exists application_role;

drop table if exists application_system;


alter table available_action 
   drop foreign key FK_AVAILABL_AVAILABLE_ROLE_AUT;

alter table available_action 
   drop foreign key FK_AVAILABL_AVAILABLE_APPLICAT;

drop table if exists available_action;

drop table if exists client;


alter table clients_authorize 
   drop foreign key FK_CLIENTS__CLIENT_AU_API_RESO;

alter table clients_authorize 
   drop foreign key FK_CLIENTS__CLIENT_AU_CLIENT;

drop table if exists clients_authorize;


alter table data_access_authorize 
   drop foreign key FK_DATA_ACC_DATA_ACCE_APPLICAT;

alter table data_access_authorize 
   drop foreign key FK_DATA_ACC_DATA_ACCE_DISTRICT;

drop table if exists data_access_authorize;


alter table department 
   drop foreign key FK_DEPARTME_PARENT_DE_DEPARTME;

alter table department 
   drop foreign key FK_DEPARTME_DEPARTMEN_ORGANIZA;

drop table if exists department;

drop table if exists district;


alter table employee 
   drop foreign key FK_EMPLOYEE_EMPLOYEE__DEPARTME;

drop table if exists employee;


alter table employee_authorize 
   drop foreign key FK_EMPLOYEE_EMPLOYEE__EMPLOYEE;

alter table employee_authorize 
   drop foreign key FK_EMPLOYEE_EMPLOYEE__APPLICAT;

drop table if exists employee_authorize;


alter table organization 
   drop foreign key FK_ORGANIZA_ORGANIZAT_DISTRICT;

alter table organization 
   drop foreign key FK_ORGANIZA_ORGANIZAT_ORGANIZA;

alter table organization 
   drop foreign key FK_ORGANIZA_PARENT_OR_ORGANIZA;

drop table if exists organization;

drop table if exists organization_type;


alter table role_authorize 
   drop foreign key FK_ROLE_AUT_ROLE_AUTH_APPLICAT;

alter table role_authorize 
   drop foreign key FK_ROLE_AUT_ROLE_AUTH_APPLICAT;

drop table if exists role_authorize;

/*==============================================================*/
/* Table: api_resource                                          */
/*==============================================================*/
create table api_resource
(
   id                   bigint not null  comment '主键',
   api_key              varchar(128) not null  comment 'ApiKey',
   api_name             varchar(200) not null  comment 'Api名称',
   display_name         varchar(100)  comment '显示名称',
   api_document_url     varchar(256)  comment 'Api文档Url',
   description          varchar(256)  comment '描述',
   is_enabled           bool not null default 1  comment '是否已启用',
   creator              bigint not null  comment '创建人',
   creation_time        datetime not null  comment '创建时间',
   last_modifier        bigint  comment '最后修改人',
   last_modification_time datetime  comment '最后修改时间',
   is_deleted           bool not null default 0  comment '是否已删除',
   deleter              bigint  comment '删除人',
   deletion_time        datetime  comment '删除时间',
   primary key (id)
);

alter table api_resource comment 'Api资源';

/*==============================================================*/
/* Table: application_action                                    */
/*==============================================================*/
create table application_action
(
   id                   bigint not null  comment '主键',
   application_system_id bigint not null  comment '应用系统主键',
   group_tag            varchar(50) not null  comment '功能组标识',
   group_name           varchar(100) not null  comment '功能组名称',
   action_tag           varchar(50) not null  comment '功能标识',
   action_name          varchar(100) not null  comment '功能名称',
   description          varchar(256)  comment '描述',
   ordinal_position     smallint not null default 0  comment '定序位置',
   is_enabled           bool not null default 1  comment '是否已启用',
   creator              bigint not null  comment '创建人',
   creation_time        datetime  comment '创建时间',
   last_modifier        bigint  comment '最后修改人',
   last_modification_time datetime  comment '最后修改时间',
   is_deleted           bool not null default 0  comment '是否已删除',
   deleter              bigint  comment '删除人',
   deletion_time        datetime  comment '删除时间',
   primary key (id)
);

alter table application_action comment '应用功能';

/*==============================================================*/
/* Table: application_menu                                      */
/*==============================================================*/
create table application_menu
(
   id                   bigint not null  comment '主键',
   application_menu_id  bigint  comment '父级应用菜单主键',
   application_system_id bigint not null  comment '所属应用',
   menu_code            varchar(50) not null  comment '菜单代码',
   menu_name            varchar(100) not null  comment '菜单名称',
   menu_url             varchar(256)  comment '菜单Url',
   menu_icon            varchar(256)  comment '菜单图标',
   description          varchar(256)  comment '描述',
   ordinal_position     smallint not null default 0  comment '定序位置',
   is_enabled           bool not null default 1  comment '是否已启用',
   current_level        smallint not null  comment '当前层级',
   creator              bigint not null  comment '创建人',
   creation_time        datetime not null  comment '创建时间',
   last_modifier        bigint  comment '最后修改人',
   last_modification_time datetime  comment '最后修改时间',
   is_deleted           bool not null default 0  comment '是否已删除',
   deleter              bigint  comment '删除人',
   deletion_time        datetime  comment '删除时间',
   primary key (id)
);

alter table application_menu comment '应用菜单';

/*==============================================================*/
/* Table: application_menu_action                               */
/*==============================================================*/
create table application_menu_action
(
   id                   bigint not null  comment '主键',
   application_menu_id  bigint not null  comment '应用菜单主键',
   application_action_id bigint not null  comment '应用功能主键',
   primary key (id)
);

alter table application_menu_action comment '应用菜单功能关联';

/*==============================================================*/
/* Table: application_role                                      */
/*==============================================================*/
create table application_role
(
   id                   bigint not null  comment '主键',
   application_system_id bigint not null  comment '所属应用主键',
   role_code            varchar(50) not null  comment '角色代码',
   role_name            varchar(100) not null  comment '角色名称',
   description          varchar(256)  comment '描述',
   is_enabled           bool not null default 1  comment '是否已启用',
   creator              bigint not null  comment '创建人',
   creation_time        datetime not null  comment '创建时间',
   last_modifier        bigint  comment '最后修改人',
   last_modification_time datetime  comment '最后修改时间',
   is_deleted           bool not null default 0  comment '是否已删除',
   deleter              bigint  comment '删除人',
   deletion_time        datetime  comment '删除时间',
   primary key (id)
);

alter table application_role comment '角色';

/*==============================================================*/
/* Table: application_system                                    */
/*==============================================================*/
create table application_system
(
   id                   bigint not null  comment '主键',
   app_no               varchar(128) not null  comment '应用编号',
   app_name             varchar(200) not null  comment '应用名称',
   display_name         varchar(100) not null  comment '显示名称',
   app_url              varchar(256) not null  comment '应用地址',
   app_icon_url         varchar(256) not null  comment '应用图标Url',
   app_secret_key       varchar(256)  comment '密钥',
   description          varchar(256)  comment '描述',
   is_enabled           bool not null default 1  comment '是否已启用',
   creator              bigint not null  comment '创建人',
   creation_time        datetime not null  comment '创建时间',
   last_modifier        bigint  comment '最后修改人',
   last_modification_time datetime  comment '最后修改时间',
   is_deleted           bool not null default 0  comment '是否已删除',
   deleter              bigint  comment '删除人',
   deletion_time        datetime  comment '删除时间',
   primary key (id)
);

alter table application_system comment '应用系统';

/*==============================================================*/
/* Table: available_action                                      */
/*==============================================================*/
create table available_action
(
   id                   bigint not null  comment '主键',
   role_authorize_id    bigint not null  comment '角色授权主键',
   application_action_id bigint not null  comment '应用功能主键',
   primary key (id)
);

alter table available_action comment '可用功能设置';

/*==============================================================*/
/* Table: client                                                */
/*==============================================================*/
create table client
(
   id                   bigint not null  comment '主键',
   client_no            varchar(128) not null  comment '客户端编号',
   client_name          varchar(200) not null  comment '客户端名称',
   client_secret_key    varchar(256) not null  comment '客户端密钥',
   token_lifetime       int not null default 300  comment 'Token有效期（分钟）',
   refresh_token_lifetime int  comment '续期Token有效期（分钟）',
   description          varchar(256)  comment '描述',
   is_enabled           bool not null default 1  comment '是否已启用',
   creator              bigint not null  comment '创建人',
   creation_time        datetime not null  comment '创建时间',
   last_modifier        bigint  comment '最后修改人',
   last_modification_time datetime  comment '最后修改时间',
   is_deleted           bool not null default 0  comment '是否已删除',
   deleter              bigint  comment '删除人',
   deletion_time        datetime  comment '删除时间',
   primary key (id)
);

alter table client comment '客户端';

/*==============================================================*/
/* Table: clients_authorize                                     */
/*==============================================================*/
create table clients_authorize
(
   id                   bigint not null  comment '主键',
   api_resource_id      bigint not null  comment 'Api资源主键',
   client_id            bigint not null  comment '客户端主键',
   primary key (id)
);

alter table clients_authorize comment '客户端授权';

/*==============================================================*/
/* Table: data_access_authorize                                 */
/*==============================================================*/
create table data_access_authorize
(
   id                   bigint not null  comment '主键',
   application_role_id  bigint not null  comment '角色主键',
   district_id          bigint not null  comment '行政区主键',
   primary key (id)
);

alter table data_access_authorize comment '数据访问授权';

/*==============================================================*/
/* Table: department                                            */
/*==============================================================*/
create table department
(
   id                   bigint not null  comment '主键',
   department_id        bigint  comment '上级部门主键',
   organization_id      bigint not null  comment '所属组织主键',
   department_name      varchar(100)  comment '部门名称',
   department_head      varchar(30)  comment '部门主管',
   ordinal_position     smallint not null default 0  comment '定序位置',
   is_enabled           bool not null default 1  comment '是否已启用',
   current_level        smallint not null  comment '当前层级',
   creator              bigint not null  comment '创建人',
   creation_time        datetime not null  comment '创建时间',
   last_modifier        bigint  comment '最后修改人',
   last_modification_time datetime  comment '最后修改时间',
   is_deleted           bool not null default 0  comment '是否已删除',
   deleter              bigint  comment '删除人',
   deletion_time        datetime  comment '删除时间',
   primary key (id)
);

alter table department comment '部门';

/*==============================================================*/
/* Table: district                                              */
/*==============================================================*/
create table district
(
   id                   bigint not null  comment '主键',
   code                 varchar(12) not null  comment '代码',
   parent_code          varchar(10)  comment '父级代码',
   name                 varchar(100) not null  comment '名称',
   ordinal_position     smallint not null default 0  comment '定序位置',
   primary key (id)
);

alter table district comment '行政区';

/*==============================================================*/
/* Table: employee                                              */
/*==============================================================*/
create table employee
(
   id                   bigint not null  comment '主键',
   department_id        bigint not null  comment '所属部门主键',
   account              varchar(30) not null  comment '账号',
   password             varchar(100) not null  comment '密码',
   employee_name        varchar(30) not null  comment '姓名',
   gender               varchar(2)  comment '性别',
   nation               varchar(100)  comment '民族',
   birth_date           varchar(20)  comment '出生日期',
   certificate_type     varchar(20)  comment '证件类型',
   certificate_no       varchar(30)  comment '证件号码',
   certificate_address  varchar(500)  comment '证件地址',
   mobile_no            varchar(20)  comment '手机号',
   contact_number       varchar(50)  comment '联系电话',
   weixin               varchar(50)  comment '微信',
   email                varchar(50)  comment '电邮',
   postal_address       varchar(200)  comment '通讯地址',
   emergency_contact    varchar(30)  comment '紧急联系人',
   emergency_contact_number varchar(50)  comment '紧急联系人电话',
   joining_date         date  comment '入职日期',
   job_position         varchar(50)  comment '工作岗位',
   job_title            varchar(50)  comment '工作职务',
   additional_information1 varchar(50)  comment '附加信息1',
   additional_information2 varchar(50)  comment '附加信息2',
   additional_information3 varchar(50)  comment '附加信息3',
   additional_information4 varchar(50)  comment '附加信息4',
   additional_information5 varchar(300)  comment '附加信息5',
   additional_information6 varchar(300)  comment '附加信息6',
   status               varchar(20) default '正常'  comment '状态:正常、停用',
   creator              bigint not null  comment '创建人',
   creation_time        datetime not null  comment '创建时间',
   last_modifier        bigint  comment '最后修改人',
   last_modification_time datetime  comment '最后修改时间',
   is_deleted           bool not null default 0  comment '是否已删除',
   deleter              bigint  comment '删除人',
   deletion_time        datetime  comment '删除时间',
   primary key (id)
);

alter table employee comment '工作人员';

/*==============================================================*/
/* Table: employee_authorize                                    */
/*==============================================================*/
create table employee_authorize
(
   id                   bigint not null  comment '主键',
   application_role_id  bigint not null  comment '角色主键',
   employee_id          bigint not null  comment '工作人员主键',
   primary key (id)
);

alter table employee_authorize comment '工作人员授权';

/*==============================================================*/
/* Table: organization                                          */
/*==============================================================*/
create table organization
(
   id                   bigint not null  comment '主键',
   district_id          bigint not null  comment '行政区主键',
   organization_id      bigint  comment '上级组织主键',
   organization_type_id bigint not null  comment '组织类型主键',
   unified_social_credit_code varchar(30) not null  comment '统一社会信用代码 ',
   organization_name    varchar(200) not null  comment '名称',
   legal_person         varchar(20)  comment '法人',
   registered_address   varchar(500)  comment '注册地址',
   contact              varchar(50) not null  comment '联系人',
   contact_number       varchar(100)  comment '联系电话',
   contact_address      varchar(500)  comment '联系地址',
   ordinal_position     smallint not null default 0  comment '定序位置',
   status               varchar(20) not null default '正常'  comment '状态:正常、停用',
   remarks              varchar(256)  comment '备注',
   current_level        smallint not null  comment '当前层级',
   creator              bigint not null  comment '创建人',
   creation_time        datetime not null  comment '创建时间',
   last_modifier        bigint  comment '最后修改人',
   last_modification_time datetime  comment '最后修改时间',
   is_deleted           bool not null default 0  comment '是否已删除',
   deleter              bigint  comment '删除人',
   deletion_time        datetime  comment '删除时间',
   primary key (id)
);

alter table organization comment '组织';

/*==============================================================*/
/* Table: organization_type                                     */
/*==============================================================*/
create table organization_type
(
   id                   bigint not null  comment '主键',
   type_name            varchar(100) not null  comment '类型名称',
   primary key (id)
);

alter table organization_type comment '组织类型';

/*==============================================================*/
/* Table: role_authorize                                        */
/*==============================================================*/
create table role_authorize
(
   id                   bigint not null  comment '主键',
   application_role_id  bigint not null  comment '角色主键',
   application_menu_id  bigint not null  comment '应用菜单主键',
   primary key (id)
);

alter table role_authorize comment '角色授权';

alter table application_action add constraint FK_APPLICAT_APPLICATI_APPLICAT foreign key (application_system_id)
      references application_system (id);

alter table application_menu add constraint FK_APPLICAT_MENU_APPL_APPLICAT foreign key (application_system_id)
      references application_system (id);

alter table application_menu add constraint FK_APPLICAT_PARENT_ME_APPLICAT foreign key (application_menu_id)
      references application_menu (id);

alter table application_menu_action add constraint FK_APPLICAT_APPLICATI_APPLICAT foreign key (application_action_id)
      references application_action (id);

alter table application_menu_action add constraint FK_APPLICAT_APPLICATI_APPLICAT foreign key (application_menu_id)
      references application_menu (id);

alter table application_role add constraint FK_APPLICAT_ROLE_APPL_APPLICAT foreign key (application_system_id)
      references application_system (id);

alter table available_action add constraint FK_AVAILABL_AVAILABLE_APPLICAT foreign key (application_action_id)
      references application_action (id);

alter table available_action add constraint FK_AVAILABL_AVAILABLE_ROLE_AUT foreign key (role_authorize_id)
      references role_authorize (id);

alter table clients_authorize add constraint FK_CLIENTS__CLIENT_AU_API_RESO foreign key (api_resource_id)
      references api_resource (id);

alter table clients_authorize add constraint FK_CLIENTS__CLIENT_AU_CLIENT foreign key (client_id)
      references client (id);

alter table data_access_authorize add constraint FK_DATA_ACC_DATA_ACCE_DISTRICT foreign key (district_id)
      references district (id);

alter table data_access_authorize add constraint FK_DATA_ACC_DATA_ACCE_APPLICAT foreign key (application_role_id)
      references application_role (id);

alter table department add constraint FK_DEPARTME_DEPARTMEN_ORGANIZA foreign key (organization_id)
      references organization (id);

alter table department add constraint FK_DEPARTME_PARENT_DE_DEPARTME foreign key (department_id)
      references department (id);

alter table employee add constraint FK_EMPLOYEE_EMPLOYEE__DEPARTME foreign key (department_id)
      references department (id);

alter table employee_authorize add constraint FK_EMPLOYEE_EMPLOYEE__EMPLOYEE foreign key (employee_id)
      references employee (id);

alter table employee_authorize add constraint FK_EMPLOYEE_EMPLOYEE__APPLICAT foreign key (application_role_id)
      references application_role (id);

alter table organization add constraint FK_ORGANIZA_ORGANIZAT_DISTRICT foreign key (district_id)
      references district (id);

alter table organization add constraint FK_ORGANIZA_ORGANIZAT_ORGANIZA foreign key (organization_type_id)
      references organization_type (id);

alter table organization add constraint FK_ORGANIZA_PARENT_OR_ORGANIZA foreign key (organization_id)
      references organization (id);

alter table role_authorize add constraint FK_ROLE_AUT_ROLE_AUTH_APPLICAT foreign key (application_menu_id)
      references application_menu (id);

alter table role_authorize add constraint FK_ROLE_AUT_ROLE_AUTH_APPLICAT foreign key (application_role_id)
      references application_role (id);
