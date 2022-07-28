## 注意
このツールを使うことで生じたあらゆる問題について、責任は持ちません

MIT Licenseです。

Easy Anti-Cheat導入後は動作しなくなる可能性があります
-> 起動前のスプラッシュ画面の間に実行すればいいそうなので対応版作りました

## 対象ユーザ

下記RyzenCPUを使用しているVRChatユーザ
※Ryzen 9 3900X/5950Xでのみ動作確認済み

Zen3
```
Ryzen 9 5950X
Ryzen 9 5900X
```

Zen2
```
Ryzen 9 3950X
Ryzen 9 3900XT
Ryzen 9 3900X
Ryzen 9 3900
Ryzen 7 3800XT
Ryzen 7 3800X
Ryzen 7 3700X
Ryzen 5 3600XT
Ryzen 5 3600X
Ryzen 5 3600
```

Zen+
```
Ryzen 7 PRO 2700X
Ryzen 7 2700X
Ryzen 7 PRO 2700
Ryzen 7 2700
Ryzen 5 2600X
Ryzen 5 PRO 2600
Ryzen 5 2600
Ryzen 5 1600AF
```

Zen
```
Ryzen 7 1800X
Ryzen 7 PRO 1700X
Ryzen 7 1700X
Ryzen 7 PRO 1700
Ryzen 7 1700
Ryzen 5 1600X
Ryzen 5 PRO 1600
Ryzen 5 1600
```


## なにこれ

Ryzenの場合、なんかVRChatで利用するCPUを単一CCX内のみにするとパフォーマンスがあがるらしい

毎回設定するのめんどくさいから自動で設定するツール

## どうやって使うの

https://github.com/thakyuu/VRC_AutoAffinitySetter_ForRyzen/releases/tag/0.0.4

VRChat起動中の、EACのスプラッシュ画面が出ている間に実行するだけ

AppBinderとかで`start_protected_game.exe`起動時に自動実行させると何も考えずに使えて便利です

https://github.com/TenteEEEE/app_binder

※EACの仕様で、VRChat本体が起動した後には使えません
