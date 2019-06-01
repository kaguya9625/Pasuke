# アプリ名 ：Pasuke(パスケ)  
 対象OS   ：iOS,Android  
 対象Ver  ：iOS:min 11 target 12.2 Android:min:21 target27  
 開発環境 ：VisualStudio2019  
 開発言語 ：C#  

# 機能一覧
・機能名           	: 機能概要  
 ・シフト追加機能    :出勤日、出勤時間、退勤時間を入力したうえでDBに登録する。  
 ・シフト一覧表示機能:DBに登録された情報を一覧にして表示する。 
 ・シフト削除機能    :選択したシフトをDBから削除することができる。  


# 画面一覧  
##・画面名   ：画面概要 
・シフト追加画面：出勤時間、退勤時間を設定した上で入力ボタンを押してカレンダーをタップするとその日付、時間でシフトが入力可能  
 シフト一覧画面 ：シフトの一覧を表示する。  

# 使用しているAPI,SDKなど  
### nuget  
 Prism.Unity.Forms(MVVM化のためPrism導入)  
 XamForms.Controls.Calendar(カレンダーのため導入)  
 PublicHoliday(カレンダー表示用に休日を取得できるパッケージ)  
 ReactiveProperty（MVVM化のために導入)  
 Realm(ローカルデータベースとして導入)  
 Xamarin.Forms.BehaviorsPack(ListViewのCommandように導入)  

# SDK  
 NETStandard.Library(初期から追加されていたもの)  


# コンセプト  

手軽に自分のシフトのスケジュールを確認できる。  
できる限りユーザーが入力する際の手間を減らす。  


# このアプリケーションの大前提として自分が使うためのものです。