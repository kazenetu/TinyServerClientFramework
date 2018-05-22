# TinyServerClientFramework
WindowsクライアントとWebAPIサーバーの簡易フレームワーク

# フレームワーク概要  
 ![イメージ](document/image/system.png)
 ## Windowsクライアント  
  * WindowForms  
    WindowsクライアントのUI部分  
    主な機能
    * 画面表示
    * 入力受付・値の検証
    * Bussinessのデータ問合せ処理・更新処理呼び出し
  * Bussiness  
    Windowsクライアントのビジネスロジック部分  
    主な機能
    * WebAPIのデータ問合せ処理・更新処理呼び出し

 ## WebAPIサーバー  
   * Controller  
     Windowsクライアントと通信するWebAPIのエンドポイント  
     主な機能
     * リクエストデータの検証
     * Transaction生成
     * 対象処理の呼び出し
     * 呼び出し結果をWindowsクライアントに返す
   * Transaction  
     DBトランザクション管理とRepositoryの処理呼び出し  
     主な機能
     * トランザクション管理
     * Repositoryの処理呼び出し(複数呼び出し可)
     * 結果をControllerに返す
   * Repository  
     DB問合せ  
     主な機能
     * SQL実行
     * 実行結果をTransactionに返す

 ## DTO  
   WindowsクライアントとWebAPIサーバー両方で利用するデータ連携用。
   * RequestDTO  
     リクエストデータ用  
     主な機能
     * 必須属性(RequiredAttribute)付きプロパティの必須入力検証
   * ResponseDTO
     レスポンスデータ用  
     主な機能
     * なし
   * TableDTO  
     Transaction・Repository間のデータ連携用  
     1テーブル・1クラス  
     ※TableDTOGeneratertツールにて自動生成
     主な機能
     * なし

# フォルダ構成  

```
Root
├─Client
│  ├─Client
│  │  └─Business
│  └─ClientFramework
│      ├─BaseClasses
│      └─ConnectLib
│      　  └─lib
├─WebAPI
│  ├─WebAPI
│  │  ├─Controllers
│  │  │  ├─V1
│  │  │  └─V2
│  │  ├─Repositories
│  │  └─Transactions
│  └─WebAPIFramework
│      ├─BaseClasses
│      ├─ConfigModel
│      ├─DB
│      └─Interfaces
└─DataTransferObjects
   ├─BaseClasses
   ├─Request
   ├─Response
   └─Tables
```

## Client(Windowsクライアント)  
   ソリューション：Client.sln
   * Client/ClientFramework  
     クライアント用フレームワークプロジェクト
     * BaseClassesフォルダ  
       * Businessクラスのスーパークラス
     * ConnectLibフォルダ  
       * WebAPI問合せ用シングルトンクラス
   * Client/Client  
     メインプロジェクト(サンプル実装)
     * Businessフォルダ  
       * ビジネスロジッククラス
     * Formクラス

## WebAPI(WebAPIサーバー)
   ソリューション：WebAPI.sln
   * WebAPI/WebAPIFramework  
     WebAPI用フレームワークプロジェクト
     * BaseClassesフォルダ  
       * Controllerのスーパークラス
       * Transactionのスーパークラス
       * Repositoryのスーパークラス
     * ConfigModelフォルダ  
       * DB単位の接続文字列格納用クラス
     * DBフォルダ  
       * SQLite用DBアクセスクラス
       * PostgreSQL用DBアクセスクラス
       * SQLServer用DBアクセスクラス
       * DBアクセスクラス生成ファクトリクラス
     * Interfacesフォルダ  
       * DBアクセスクラス用インターフェース
       * Repository用インターフェース

   * WebAPI/WebAPI  
     メインプロジェクト(サンプル実装)
     * Controllersフォルダ
       * バージョン(例：v1)フォルダ
         * コントローラークラス
     * Transactionsフォルダ
       * トランザクションクラス
     * Repositoriesフォルダ
       * リポジトリクラス
 
## DataTransferObjects(Windowsクライアント・WebAPIサーバー共有)
     
   ソリューション：なし
   * DataTransferObjectsプロジェクト
     * BaseClassesフォルダ  
       * Requestのスーパークラス
       * Responseのスーパークラス
       * Tableのスーパークラス
     * Requestフォルダ  
       * Request用DTO(サンプル実装)
     * Responseフォルダ  
       * Responset用DTO(サンプル実装)
     * Tablesフォルダ  
       * テーブル用DTO(サンプル実装)  
       ※TableDTOGeneratertツールにて自動生成

