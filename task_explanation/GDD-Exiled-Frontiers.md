# Game Design Document: Exiled Frontiers

**Oyun Adi:** Exiled Frontiers (Calisma Adi)

---

## 1. Ust Duzey Konsept ve Fantazi

**Ana Konsept:** Exiled Frontiers, yavas tempolu, dusunceye dayali bir sehir kurma hayatta kalma simülasyonudur. Oyuncular, sert ve vahsi bir dogada yeni bir medeniyet kurmaya calisan bir grup surgun yerlesimciyi yonlendirir. Oyun, sistemik kaynak yonetimi, nufus surdurulebilirligi ve cevre zorluklarina karsi uzun vadeli stratejik planlamayi on plana cikarir.

**Tur:** 2D Izometrik Sehir Kurma Hayatta Kalma Stratejisi

**Fantazi:** Unutulmus bir diyara surulmus umutsuz bir grup surgunluyu yonetirsiniz. Ellerinde yalnizca dayanikliliklari ve birkac yetersiz malzemeyle yeni bir yuva kurmak zorundadirlar. Bu, hayatta kalma, buyume ve insan ile doga arasindaki hassas denge hikayesidir. Insanlariniz dogacak, buyuyecek, calisacak ve olecek; mevsimlerin amansiz dongusu, hastalik tehdidi ve surekli kaynak kitligi ile yuz yuze gelecekler.

**Ton:** Dusunceli, atmosferik, hayatta kalma odakli, stratejik, tefekkure dayali.

---

## 2. Hedef Platform ve Kitle

**Platform:** Web (HTML5) - *Not: Case kapsaminda istediginiz platformu hedefleyebilirsiniz.*

**Hedef Kitle:**
- Derin, sistemik simülasyon ve yonetim oyunlarindan keyif alan oyuncular (orn. Banished, Frostpunk, Cities: Skylines)
- Hayatta kalma stratejisi ve sehir kurma turlerinin hayranlari
- Hizli aksiyon yerine yavas tempolu, stratejik oynanisi tercih eden oyuncular

---

## 3. Ana Dongu ve Ilerleme

### Ana Oynanis Dongusu

1. **Topla:** Oyuncular yerlesikcileri hammadde toplamaya yonlendirir (odun, tas, cilek, su)
2. **Ins Et:** Toplanan kaynaklar ile binalari insa eder (barinaklar, uretim tesisleri, depolar)
3. **Ata:** Yerlesikciler binalardaki belirli islere atanir (oduncu, madenci, ciftci, insaatci, sifaci)
4. **Uret:** Atanmis isciler islenmis kaynaklar uretir (kereste, alet, pisirmis yemek, ilac, giysi)
5. **Hayatta Kal:** Yerlesim yeri nufusu icin yiyecek, sicaklik, saglik ve barinak dengesini korumalidir
6. **Genisle:** Nufus buyudukce ve kaynaklar izin verdikce yerlesim yeri genislenir, yeni teknolojiler acilir

### Ilerleme Sistemi
- **Yerlesim Kademeleri:** Kamp → Mezra → Koy → Kasaba
- **Teknoloji Agaci:** Yeni yapi planlari, gelistirilmis alet verimliligi, gelismis tarim teknikleri
- **Nufus Kilometre Taslari:** Belirli nufus sayilari etkinlikleri tetikler
- **Harita Genislemesi:** Yeni alanlar acmak icin kaynak yatirimi

---

## 4. Mekanikler

### Temel Mekanikler

- **Yerlesikciler Hareketi:** Otomatik yol bulma, oyuncu tiklama ile gorev/hedef atama
- **Toplanabilirler:** Kaynak dugumleri (agaclar, kayalar, cilek calilari) tiklanabilir nesneler
- **Ilerleme Takipcisi:** UI paneli - nufus, kaynak stoklari, mevsim, mutluluk/istikrar
- **Cevre Hikaye Anlatimi:** Antik kalintilar, dogal olusumlar, esya parcalari
- **Arazi Sistemi:** Grid tabanli 2D izometrik, degisen yukselti, biyomlar (orman, ova, dag, nehir)
- **Gunduz/Gece Dongusu:** Gorsel ve mekanik dongu - gunduz daha iyi verimlilik, gece azalmis verimlilik
- **Atmosferik Sis:** Uzak alanlari gorunur kilar veya hava olaylari sirasinda yogunlasir
- **Kamera Yumusatma:** Haritada kaydirma/yakinlastirma icin yumusak kamera hareketi
- **Kamera Sinirlari:** Kamera oynanabilir harita alanina sinirlidir
- **Zorluk Secenekleri:** Kolay, Normal, Zor - kaynak kitligi, hastalik sikligi, hava siddeti etkiler
- **Ilerleme Kaydetme:** Tum oyun ilerlemesi otomatik kaydedilir ve yuklenir

### UI/UX Mekanikleri
- **HUD:** Minimalist, kritik bilgiler her zaman gorunur
- **Kaynak Kazanim Popup'lari:** "+5 Odun" gibi gosterimler
- **Kilometre Tasi Kutlamasi:** Onemli basarimlar icin gorsel/sesli geri bildirim
- **UI Gecisleri:** Menu acma/kapama icin yumusak animasyonlar
- **Buton Geri Bildirimi:** Hover, tikla durumlari icin gorsel ve sesli geri bildirim

---

## 5. Sistemler

### Ekonomi ve Kaynak Sistemi
- **Baglantili Kaynak Zincirleri:** Hammadde (Odun, Tas, Cilek, Su, Kil, Cevher) → islenmis mallar (Kereste, Alet, Pisirmis Yemek, Seramik, Metal Kulce, Ilac, Giysi)
- **Uretim Binalari:** Her binanin girdi gereksinimleri ve cikti urunleri vardir
- **Depolama Sistemi:** Kaynaklar belirlenmis binalarda depolanmalidir - sinirli kapasite
- **Kaynak Tukenme ve Yenilenme:** Hammaddeler zamanla tukenir, bazilari yenilenebilir
- **Alet Dayanakliligi:** Aletler isciler tarafindan tuketilir, surekli uretim gerekir

### Nufus Sistemi
- **Yerlesikcilerin Yasam Dongusu:** Dogum, buyume, yetiskinlik, yaslilik, olum
- **Isgucu Katilimi:** Cocuklar calismaz, yetiskinler ana isgucu, yasilar azaltilmis verimlilik
- **Ihtiyaclar ve Mutluluk:** Yiyecek, Su, Barinak, Sicaklik, Saglik, Giysi
- **Is Atama:** Oyuncu yerlesikcileri islere atar, deneyim kazanirlar
- **Hastalik ve Yaralanma:** Tibbi bakim gerektirir, isgucu azalir, bulasmaz

### Hayatta Kalma ve Risk Sistemi
- **Mevsim Dongusu:** Ilkbahar, Yaz, Sonbahar, Kis - kaynak etkisi, ciftcilik, isci verimliligi
- **Cevre Tehlikeleri:** Sert hava, dogal afetler, yaban hayati (savas yok)
- **Kitlik:** Yetersiz yiyecek → aclik → hastalik → olum
- **Hastalik Salgini:** Kotu hijyen, ilac eksikligi → yaygin hastalik
- **Soguk ve Maruz Kalma:** Barinak/giysi/yakit eksikligi → hastalik ve olum

### Yapi Sistemi
- **Yerlesim:** 2D izometrik grid, arazi kisitlamalari
- **Insaat:** Kaynak ve insaatci isgucu gerektirir, asamali ilerleme
- **Bakim:** Binalar zamanla bozulur, onarim gerektirir
- **Yukseltme:** Bazi binalar verimlilik/kapasite artisi icin yukseltilebilir

### Kesif ve Genisleme Sistemi
- **Savas Sisi:** Harita baslangiçta karanlikta, kesfettikce acilir
- **Kaynak Kesfi:** Genisleme yeni kaynaklar, yapi alanlari ve hikaye ogeleri ortaya cikarir
- **Ileri Karakol:** Uzak bolgelerde kaynak toplama icin kucuk karakollar kurulabilir

---

## 6. Seviye/Karsilasma Yapisi ve Tempo

- **Tek Harita:** Tum oyun tek, kalici bir 2D izometrik haritada gerceklesir
- **Yavas ve Stratejik:** Kararlar uzun vadeli sonuclar tasir
- **Ortaya Cikan Zorluklar:** Zorluklar birbirine bagli sistemlerden dogal olarak ortaya cikar
- **Mevsimsel Ritim:** Gunduz/gece ve mevsim donguleri oynanis ritmini belirler

---

## 7. Engel Taksonomisi

Dogrudan savas veya geleneksel "dusman" yoktur. Tum zorluklar cevresel, sistemik veya ekonomiktir:

- **Cevre Engelleri:** Sert mevsimler, elverissiz arazi, kaynak kitligi, dogal afetler
- **Sistemik Engeller:** Kitlik, hastalik salgini, soguk/maruz kalma, kaynak tukenme, nufus dususu
- **Ekonomik Engeller:** Verimsiz uretim zincirleri, isgucu eksikligi, dengesizlik

---

## 8. Ilerleme ve Meta

- **Erken Oyun:** Temel ihtiyaclarin guvence altina alinmasi, ilk uretim zincirleri
- **Orta Oyun:** Genisleme, nufus artisi, temel teknolojiler, uzmanlasmis isciler
- **Gec Oyun:** Kendine yeterlilik, gelismis yapilar, ust duzey teknolojiler
- **Son Oyun:** Buyuk olcude kendi kendini yoneten ve direncli yerlesim yeri

---

## 9. Sanat Yonu

- **Stil:** Stilize low-poly estetik, 2D izometrik perspektifte render
- **Palet:** Susturulmus ortacag renkleri - dogal tonlar (yesiller, kahverengiler, griler, soluk maviler)
- **Genel His:** Saglam ve hafif kasvetli, hayatta kalma temasini yansitiyor

### Temel Asset Gereksinimleri
- Arazi tile setleri (cimlik, orman, kayalik, dag, nehir, batak)
- Agaclar (en az 3 tur: cam, yaprak doken, cali)
- Kayalar ve cicekler (en az 5 tur)
- Toplanabilir kaynaklar (en az 10: odun, tas, cilek, su, kil, cevher, kereste, alet, yemek, ilac)
- Hikaye isaretleri (en az 5: kalintilar, heykel, tapinca, magara, kamp izi)
- Isik efektleri, HUD ogeleri, gok kubbesi arka plani
- Ses efektleri ve ortam muzigi

---

## 10. Kontroller ve UX

### Masaustu Kontroller
- **Sol Tikla:** Sec, onayla
- **Sag Tikla:** Secimi kaldir, iptal, baglam menusu
- **Fare Tekeri:** Yakinlastir/uzaklastir
- **Orta Tikla/Surukle:** Kamera kaydirma
- **WASD / Yön Tuslari:** Kamera kaydirma
- **Sayi Tuslari (1-9):** Bina kategorileri icin kisayollar
- **Escape:** Menu kapat, secimi kaldir
- **Bosluk:** Duraklat/devam et

### UX Ilkeleri
- Minimalist UI, netligi on plana cikarir
- Etkilesim icin acik gorsel ipuclari
- Her oyuncu eylemi icin aninda gorsel ve sesli geri bildirim
- Bilgi hiyerarsisi: onemli uyarilar belirgin ama rahatsiz etmeyen

---

*Bu GDD, R2D Games teknik case gorevi kapsaminda saglanmistir. Tum ozelliklerin implemente edilmesi beklenmemektedir - onceliklendirme size aittir.*
