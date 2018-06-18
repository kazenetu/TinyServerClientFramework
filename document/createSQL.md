# テストテーブル作成DDL
- SQLServer
    ```
    -- ユーザーマスター
    create table MT_USER (
      USER_ID VARCHAR(30)
      , USER_NAME VARCHAR(30)
      , PASSWORD VARCHAR(100)
      , DEL_FLAG CHAR(1)
      , ENTRY_USER VARCHAR(30)
      , ENTRY_DATE DATETIME
      , MOD_USER VARCHAR(30)
      , MOD_DATE DATETIME
      , MOD_VERSION INTEGER
      , constraint MT_USER_PKC primary key (USER_ID)
    ) ;

    EXECUTE sp_addextendedproperty N'MS_Description', N'ユーザーマスター', N'SCHEMA', N'dbo', N'TABLE', N'MT_USER', NULL, NULL;
    EXECUTE sp_addextendedproperty N'MS_Description', N'ユーザーID', N'SCHEMA', N'dbo', N'TABLE', N'MT_USER', N'COLUMN', N'USER_ID';
    EXECUTE sp_addextendedproperty N'MS_Description', N'ユーザー名', N'SCHEMA', N'dbo', N'TABLE', N'MT_USER', N'COLUMN', N'USER_NAME';
    EXECUTE sp_addextendedproperty N'MS_Description', N'パスワード', N'SCHEMA', N'dbo', N'TABLE', N'MT_USER', N'COLUMN', N'PASSWORD';
    EXECUTE sp_addextendedproperty N'MS_Description', N'削除フラグ', N'SCHEMA', N'dbo', N'TABLE', N'MT_USER', N'COLUMN', N'DEL_FLAG';
    EXECUTE sp_addextendedproperty N'MS_Description', N'登録ユーザー', N'SCHEMA', N'dbo', N'TABLE', N'MT_USER', N'COLUMN', N'ENTRY_USER';
    EXECUTE sp_addextendedproperty N'MS_Description', N'登録日時', N'SCHEMA', N'dbo', N'TABLE', N'MT_USER', N'COLUMN', N'ENTRY_DATE';
    EXECUTE sp_addextendedproperty N'MS_Description', N'更新ユーザー', N'SCHEMA', N'dbo', N'TABLE', N'MT_USER', N'COLUMN', N'MOD_USER';
    EXECUTE sp_addextendedproperty N'MS_Description', N'更新日時', N'SCHEMA', N'dbo', N'TABLE', N'MT_USER', N'COLUMN', N'MOD_DATE';
    EXECUTE sp_addextendedproperty N'MS_Description', N'更新バージョン', N'SCHEMA', N'dbo', N'TABLE', N'MT_USER', N'COLUMN', N'MOD_VERSION';

    -- 注文テーブル
    create table T_ORDER (
      ORDER_NO INTEGER not null
      , ORDER_USER_ID VARCHAR(30)
      , MOD_VERSION INTEGER
      , constraint T_ORDER_PKC primary key (ORDER_NO)
    ) ;

    EXECUTE sp_addextendedproperty N'MS_Description', N'注文', N'SCHEMA', N'dbo', N'TABLE', N'T_ORDER', NULL, NULL;
    EXECUTE sp_addextendedproperty N'MS_Description', N'注文No', N'SCHEMA', N'dbo', N'TABLE', N'T_ORDER', N'COLUMN', N'ORDER_NO';
    EXECUTE sp_addextendedproperty N'MS_Description', N'注文者ID', N'SCHEMA', N'dbo', N'TABLE', N'T_ORDER', N'COLUMN', N'ORDER_USER_ID';
    EXECUTE sp_addextendedproperty N'MS_Description', N'更新バージョン', N'SCHEMA', N'dbo', N'TABLE', N'T_ORDER', N'COLUMN', N'MOD_VERSION';
    ```

- SQLite
    ```
    -- ユーザーマスター
    create table MT_USER (
        USER_ID NVARCHAR
        , USER_NAME NVARCHAR
        , PASSWORD NVARCHAR
        , DEL_FLAG NCHAR
        , ENTRY_USER NVARCHAR
        , ENTRY_DATE DATETIME
        , MOD_USER NVARCHAR
        , MOD_DATE DATETIME
        , MOD_VERSION INT
        , primary key (USER_ID)
      );

    -- 注文テーブル
    create table T_ORDER (
      ORDER_NO INTEGER not null
      , ORDER_USER_ID CHARACTER VARYING
      , MOD_VERSION INTEGER
      , primary key (ORDER_NO)
    );
    ```

- PostgeSQL
    ```
    -- ユーザーマスター
    create table MT_USER (
      USER_ID character varying(30)
      , USER_NAME character varying(30)
      , PASSWORD character varying(100)
      , DEL_FLAG character(1)
      , ENTRY_USER character varying(30)
      , ENTRY_DATE timestamp
      , MOD_USER character varying(30)
      , MOD_DATE timestamp
      , MOD_VERSION integer
      , constraint MT_USER_PKC primary key (USER_ID)
    ) ;

    comment on table MT_USER is 'ユーザーマスター';
    comment on column MT_USER.USER_ID is 'ユーザーID';
    comment on column MT_USER.USER_NAME is 'ユーザー名';
    comment on column MT_USER.PASSWORD is 'パスワード';
    comment on column MT_USER.DEL_FLAG is '削除フラグ';
    comment on column MT_USER.ENTRY_USER is '登録ユーザー';
    comment on column MT_USER.ENTRY_DATE is '登録日時';
    comment on column MT_USER.MOD_USER is '更新ユーザー';
    comment on column MT_USER.MOD_DATE is '更新日時';
    comment on column MT_USER.MOD_VERSION is '更新バージョン';

    -- 注文テーブル
    create table T_ORDER (
      ORDER_NO integer not null
      , ORDER_USER_ID character varying(30)
      , MOD_VERSION integer
      , constraint T_ORDER_PKC primary key (ORDER_NO)
    ) ;

    comment on table T_ORDER is '注文';
    comment on column T_ORDER.ORDER_NO is '注文No';
    comment on column T_ORDER.ORDER_USER_ID is '注文者ID';
    comment on column T_ORDER.MOD_VERSION is '更新バージョン';
    ```

# テストデータ
- testユーザー
    ```
    insert into MT_USER(USER_ID,USER_NAME,PASSWORD,DEL_FLAG,ENTRY_USER,ENTRY_DATE,MOD_USER,MOD_DATE,MOD_VERSION) values ('test','テストユーザー','Z5SMGm/kEGTiZP8tHwuWSwYWFguMP7/qJOnLNL1u4is=','0','','2018/01/21 17:32:00',null,null,1);
    ```

- 注文データ
    ```
    insert into T_ORDER(ORDER_NO,ORDER_USER_ID,MOD_VERSION) values (1,'None',1);
    insert into T_ORDER(ORDER_NO,ORDER_USER_ID,MOD_VERSION) values (2,'aaa',1);
    insert into T_ORDER(ORDER_NO,ORDER_USER_ID,MOD_VERSION) values (3,'test',1);
    insert into T_ORDER(ORDER_NO,ORDER_USER_ID,MOD_VERSION) values (4,'test2',1);
    ```
