# テストテーブル作成DDL
- SQLServer
  ```
  create table [dbo].MT_USER (
    USER_ID nvarchar(30) not null
    , USER_NAME nvarchar(30)
    , PASSWORD nvarchar(100)
    , DEL_FLAG nchar(1)
    , ENTRY_USER nvarchar(30)
    , ENTRY_DATE datetime
    , MOD_USER nvarchar(30)
    , MOD_DATE datetime
    , MOD_VERSION int
    , primary key (USER_ID)
  );
  ```

- SQLite
  ```
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
  ```

- PostgeSQL
  ```
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
  ```

# テストデータ
- testユーザー
  ```
  insert into MT_USER(USER_ID,USER_NAME,PASSWORD,DEL_FLAG,ENTRY_USER,ENTRY_DATE,MOD_USER,MOD_DATE,MOD_VERSION) values ('test','テストユーザー','Z5SMGm/kEGTiZP8tHwuWSwYWFguMP7/qJOnLNL1u4is=','0','','2018/01/21 17:32:00',null,null,1);
  ```
