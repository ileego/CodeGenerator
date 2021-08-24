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
   id                   bigint not null  comment '����',
   api_key              varchar(128) not null  comment 'ApiKey',
   api_name             varchar(200) not null  comment 'Api����',
   display_name         varchar(100)  comment '��ʾ����',
   api_document_url     varchar(256)  comment 'Api�ĵ�Url',
   description          varchar(256)  comment '����',
   is_enabled           bool not null default 1  comment '�Ƿ�������',
   creator              bigint not null  comment '������',
   creation_time        datetime not null  comment '����ʱ��',
   last_modifier        bigint  comment '����޸���',
   last_modification_time datetime  comment '����޸�ʱ��',
   is_deleted           bool not null default 0  comment '�Ƿ���ɾ��',
   deleter              bigint  comment 'ɾ����',
   deletion_time        datetime  comment 'ɾ��ʱ��',
   primary key (id)
);

alter table api_resource comment 'Api��Դ';

/*==============================================================*/
/* Table: application_action                                    */
/*==============================================================*/
create table application_action
(
   id                   bigint not null  comment '����',
   application_system_id bigint not null  comment 'Ӧ��ϵͳ����',
   group_tag            varchar(50) not null  comment '�������ʶ',
   group_name           varchar(100) not null  comment '����������',
   action_tag           varchar(50) not null  comment '���ܱ�ʶ',
   action_name          varchar(100) not null  comment '��������',
   description          varchar(256)  comment '����',
   ordinal_position     smallint not null default 0  comment '����λ��',
   is_enabled           bool not null default 1  comment '�Ƿ�������',
   creator              bigint not null  comment '������',
   creation_time        datetime  comment '����ʱ��',
   last_modifier        bigint  comment '����޸���',
   last_modification_time datetime  comment '����޸�ʱ��',
   is_deleted           bool not null default 0  comment '�Ƿ���ɾ��',
   deleter              bigint  comment 'ɾ����',
   deletion_time        datetime  comment 'ɾ��ʱ��',
   primary key (id)
);

alter table application_action comment 'Ӧ�ù���';

/*==============================================================*/
/* Table: application_menu                                      */
/*==============================================================*/
create table application_menu
(
   id                   bigint not null  comment '����',
   application_menu_id  bigint  comment '����Ӧ�ò˵�����',
   application_system_id bigint not null  comment '����Ӧ��',
   menu_code            varchar(50) not null  comment '�˵�����',
   menu_name            varchar(100) not null  comment '�˵�����',
   menu_url             varchar(256)  comment '�˵�Url',
   menu_icon            varchar(256)  comment '�˵�ͼ��',
   description          varchar(256)  comment '����',
   ordinal_position     smallint not null default 0  comment '����λ��',
   is_enabled           bool not null default 1  comment '�Ƿ�������',
   current_level        smallint not null  comment '��ǰ�㼶',
   creator              bigint not null  comment '������',
   creation_time        datetime not null  comment '����ʱ��',
   last_modifier        bigint  comment '����޸���',
   last_modification_time datetime  comment '����޸�ʱ��',
   is_deleted           bool not null default 0  comment '�Ƿ���ɾ��',
   deleter              bigint  comment 'ɾ����',
   deletion_time        datetime  comment 'ɾ��ʱ��',
   primary key (id)
);

alter table application_menu comment 'Ӧ�ò˵�';

/*==============================================================*/
/* Table: application_menu_action                               */
/*==============================================================*/
create table application_menu_action
(
   id                   bigint not null  comment '����',
   application_menu_id  bigint not null  comment 'Ӧ�ò˵�����',
   application_action_id bigint not null  comment 'Ӧ�ù�������',
   primary key (id)
);

alter table application_menu_action comment 'Ӧ�ò˵����ܹ���';

/*==============================================================*/
/* Table: application_role                                      */
/*==============================================================*/
create table application_role
(
   id                   bigint not null  comment '����',
   application_system_id bigint not null  comment '����Ӧ������',
   role_code            varchar(50) not null  comment '��ɫ����',
   role_name            varchar(100) not null  comment '��ɫ����',
   description          varchar(256)  comment '����',
   is_enabled           bool not null default 1  comment '�Ƿ�������',
   creator              bigint not null  comment '������',
   creation_time        datetime not null  comment '����ʱ��',
   last_modifier        bigint  comment '����޸���',
   last_modification_time datetime  comment '����޸�ʱ��',
   is_deleted           bool not null default 0  comment '�Ƿ���ɾ��',
   deleter              bigint  comment 'ɾ����',
   deletion_time        datetime  comment 'ɾ��ʱ��',
   primary key (id)
);

alter table application_role comment '��ɫ';

/*==============================================================*/
/* Table: application_system                                    */
/*==============================================================*/
create table application_system
(
   id                   bigint not null  comment '����',
   app_no               varchar(128) not null  comment 'Ӧ�ñ��',
   app_name             varchar(200) not null  comment 'Ӧ������',
   display_name         varchar(100) not null  comment '��ʾ����',
   app_url              varchar(256) not null  comment 'Ӧ�õ�ַ',
   app_icon_url         varchar(256) not null  comment 'Ӧ��ͼ��Url',
   app_secret_key       varchar(256)  comment '��Կ',
   description          varchar(256)  comment '����',
   is_enabled           bool not null default 1  comment '�Ƿ�������',
   creator              bigint not null  comment '������',
   creation_time        datetime not null  comment '����ʱ��',
   last_modifier        bigint  comment '����޸���',
   last_modification_time datetime  comment '����޸�ʱ��',
   is_deleted           bool not null default 0  comment '�Ƿ���ɾ��',
   deleter              bigint  comment 'ɾ����',
   deletion_time        datetime  comment 'ɾ��ʱ��',
   primary key (id)
);

alter table application_system comment 'Ӧ��ϵͳ';

/*==============================================================*/
/* Table: available_action                                      */
/*==============================================================*/
create table available_action
(
   id                   bigint not null  comment '����',
   role_authorize_id    bigint not null  comment '��ɫ��Ȩ����',
   application_action_id bigint not null  comment 'Ӧ�ù�������',
   primary key (id)
);

alter table available_action comment '���ù�������';

/*==============================================================*/
/* Table: client                                                */
/*==============================================================*/
create table client
(
   id                   bigint not null  comment '����',
   client_no            varchar(128) not null  comment '�ͻ��˱��',
   client_name          varchar(200) not null  comment '�ͻ�������',
   client_secret_key    varchar(256) not null  comment '�ͻ�����Կ',
   token_lifetime       int not null default 300  comment 'Token��Ч�ڣ����ӣ�',
   refresh_token_lifetime int  comment '����Token��Ч�ڣ����ӣ�',
   description          varchar(256)  comment '����',
   is_enabled           bool not null default 1  comment '�Ƿ�������',
   creator              bigint not null  comment '������',
   creation_time        datetime not null  comment '����ʱ��',
   last_modifier        bigint  comment '����޸���',
   last_modification_time datetime  comment '����޸�ʱ��',
   is_deleted           bool not null default 0  comment '�Ƿ���ɾ��',
   deleter              bigint  comment 'ɾ����',
   deletion_time        datetime  comment 'ɾ��ʱ��',
   primary key (id)
);

alter table client comment '�ͻ���';

/*==============================================================*/
/* Table: clients_authorize                                     */
/*==============================================================*/
create table clients_authorize
(
   id                   bigint not null  comment '����',
   api_resource_id      bigint not null  comment 'Api��Դ����',
   client_id            bigint not null  comment '�ͻ�������',
   primary key (id)
);

alter table clients_authorize comment '�ͻ�����Ȩ';

/*==============================================================*/
/* Table: data_access_authorize                                 */
/*==============================================================*/
create table data_access_authorize
(
   id                   bigint not null  comment '����',
   application_role_id  bigint not null  comment '��ɫ����',
   district_id          bigint not null  comment '����������',
   primary key (id)
);

alter table data_access_authorize comment '���ݷ�����Ȩ';

/*==============================================================*/
/* Table: department                                            */
/*==============================================================*/
create table department
(
   id                   bigint not null  comment '����',
   department_id        bigint  comment '�ϼ���������',
   organization_id      bigint not null  comment '������֯����',
   department_name      varchar(100)  comment '��������',
   department_head      varchar(30)  comment '��������',
   ordinal_position     smallint not null default 0  comment '����λ��',
   is_enabled           bool not null default 1  comment '�Ƿ�������',
   current_level        smallint not null  comment '��ǰ�㼶',
   creator              bigint not null  comment '������',
   creation_time        datetime not null  comment '����ʱ��',
   last_modifier        bigint  comment '����޸���',
   last_modification_time datetime  comment '����޸�ʱ��',
   is_deleted           bool not null default 0  comment '�Ƿ���ɾ��',
   deleter              bigint  comment 'ɾ����',
   deletion_time        datetime  comment 'ɾ��ʱ��',
   primary key (id)
);

alter table department comment '����';

/*==============================================================*/
/* Table: district                                              */
/*==============================================================*/
create table district
(
   id                   bigint not null  comment '����',
   code                 varchar(12) not null  comment '����',
   parent_code          varchar(10)  comment '��������',
   name                 varchar(100) not null  comment '����',
   ordinal_position     smallint not null default 0  comment '����λ��',
   primary key (id)
);

alter table district comment '������';

/*==============================================================*/
/* Table: employee                                              */
/*==============================================================*/
create table employee
(
   id                   bigint not null  comment '����',
   department_id        bigint not null  comment '������������',
   account              varchar(30) not null  comment '�˺�',
   password             varchar(100) not null  comment '����',
   employee_name        varchar(30) not null  comment '����',
   gender               varchar(2)  comment '�Ա�',
   nation               varchar(100)  comment '����',
   birth_date           varchar(20)  comment '��������',
   certificate_type     varchar(20)  comment '֤������',
   certificate_no       varchar(30)  comment '֤������',
   certificate_address  varchar(500)  comment '֤����ַ',
   mobile_no            varchar(20)  comment '�ֻ���',
   contact_number       varchar(50)  comment '��ϵ�绰',
   weixin               varchar(50)  comment '΢��',
   email                varchar(50)  comment '����',
   postal_address       varchar(200)  comment 'ͨѶ��ַ',
   emergency_contact    varchar(30)  comment '������ϵ��',
   emergency_contact_number varchar(50)  comment '������ϵ�˵绰',
   joining_date         date  comment '��ְ����',
   job_position         varchar(50)  comment '������λ',
   job_title            varchar(50)  comment '����ְ��',
   additional_information1 varchar(50)  comment '������Ϣ1',
   additional_information2 varchar(50)  comment '������Ϣ2',
   additional_information3 varchar(50)  comment '������Ϣ3',
   additional_information4 varchar(50)  comment '������Ϣ4',
   additional_information5 varchar(300)  comment '������Ϣ5',
   additional_information6 varchar(300)  comment '������Ϣ6',
   status               varchar(20) default '����'  comment '״̬:������ͣ��',
   creator              bigint not null  comment '������',
   creation_time        datetime not null  comment '����ʱ��',
   last_modifier        bigint  comment '����޸���',
   last_modification_time datetime  comment '����޸�ʱ��',
   is_deleted           bool not null default 0  comment '�Ƿ���ɾ��',
   deleter              bigint  comment 'ɾ����',
   deletion_time        datetime  comment 'ɾ��ʱ��',
   primary key (id)
);

alter table employee comment '������Ա';

/*==============================================================*/
/* Table: employee_authorize                                    */
/*==============================================================*/
create table employee_authorize
(
   id                   bigint not null  comment '����',
   application_role_id  bigint not null  comment '��ɫ����',
   employee_id          bigint not null  comment '������Ա����',
   primary key (id)
);

alter table employee_authorize comment '������Ա��Ȩ';

/*==============================================================*/
/* Table: organization                                          */
/*==============================================================*/
create table organization
(
   id                   bigint not null  comment '����',
   district_id          bigint not null  comment '����������',
   organization_id      bigint  comment '�ϼ���֯����',
   organization_type_id bigint not null  comment '��֯��������',
   unified_social_credit_code varchar(30) not null  comment 'ͳһ������ô��� ',
   organization_name    varchar(200) not null  comment '����',
   legal_person         varchar(20)  comment '����',
   registered_address   varchar(500)  comment 'ע���ַ',
   contact              varchar(50) not null  comment '��ϵ��',
   contact_number       varchar(100)  comment '��ϵ�绰',
   contact_address      varchar(500)  comment '��ϵ��ַ',
   ordinal_position     smallint not null default 0  comment '����λ��',
   status               varchar(20) not null default '����'  comment '״̬:������ͣ��',
   remarks              varchar(256)  comment '��ע',
   current_level        smallint not null  comment '��ǰ�㼶',
   creator              bigint not null  comment '������',
   creation_time        datetime not null  comment '����ʱ��',
   last_modifier        bigint  comment '����޸���',
   last_modification_time datetime  comment '����޸�ʱ��',
   is_deleted           bool not null default 0  comment '�Ƿ���ɾ��',
   deleter              bigint  comment 'ɾ����',
   deletion_time        datetime  comment 'ɾ��ʱ��',
   primary key (id)
);

alter table organization comment '��֯';

/*==============================================================*/
/* Table: organization_type                                     */
/*==============================================================*/
create table organization_type
(
   id                   bigint not null  comment '����',
   type_name            varchar(100) not null  comment '��������',
   primary key (id)
);

alter table organization_type comment '��֯����';

/*==============================================================*/
/* Table: role_authorize                                        */
/*==============================================================*/
create table role_authorize
(
   id                   bigint not null  comment '����',
   application_role_id  bigint not null  comment '��ɫ����',
   application_menu_id  bigint not null  comment 'Ӧ�ò˵�����',
   primary key (id)
);

alter table role_authorize comment '��ɫ��Ȩ';

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
