# Exiled Frontiers (Prototype)

**Exiled Frontiers**, kaynak yönetimi ve işçi yapay zekasına odaklanan, izometrik kamera açısına sahip bir Gerçek Zamanlı Strateji (RTS) ve Şehir Kurma prototipidir. 

Bu proje; hafta içi tam zamanlı mesai ve hafta sonu gerçekleşen bir iş seyahati gibi oldukça kısıtlı bir zaman diliminde, tek başıma geliştirilmiştir. Bu nedenle oyunun görsel sanatından (art polish) ziyade; sistem mimarisi, modülerlik, "Core Loop" (temel oyun döngüsü) ve yapay zeka (NavMesh) entegrasyonuna odaklanılmıştır. Geçici olarak 3D primitive objeler kullanılarak sistemin test edilebilir olması sağlanmıştır.

## ⛺ Oynanış Özeti
Oyunda farklı uzmanlıklara (Lumberjack, Miner, Builder vb.) sahip işçileri yöneterek haritadaki çeşitli kaynakları (Odun, Taş, Demir, Meyve) toplamanız gerekmektedir. Toplanan kaynaklar global envanterde birikir. Bu kaynakları kullanarak:
* Crafting (Üretim) menüsünden yeni eşyalar üretebilir,
* Build (İnşaat) menüsünden Grid (Izgara) sistemine uygun şekilde kendi binalarınızın temellerini atabilirsiniz.

Bir binanın temelini attığınızda (hologramı yerleştirdiğinizde), "Builder" mesleğine sahip işçiniz otomatik olarak şantiyeye koşar ve binayı fiziksel olarak inşa eder.

## ⌨️ Kontroller

* **Sol Tık:** İşçileri seçer (Kafalarındaki yazılardan meslekleri görülebilir). İşçinin üzerine tıklamak yerine boşluğa sol tıklamak seçimi temizler.
* **Shift + Sol Tık:** Birden fazla işçiyi aynı anda (Multi-select) seçmenizi sağlar.
* **Sağ Tık (Zemine):** Seçili işçilere yürüme emri verir.
* **Sağ Tık (Kaynağa):** Seçili işçilere o kaynağı toplama emri verir.
* **I Tuşu:** Envanter, Craft ve Build sekmelerinin bulunduğu ana arayüzü açar/kapatır.
* **Q ve E Tuşları (İnşaat Önizlemesindeyken):** Yerleştirilecek binanın hologramını 45 derecelik açılarla döndürür.
* **Sol Tık (İnşaat Önizlemesindeyken):** Binanın temelini grid üzerine oturtur ve inşaat emrini başlatır.
* **ESC:** Oyundan çıkıp Ana Menüye (Main Menu) dönmeyi sağlar.

## 🛠️ Kurulum ve Build Alma (Geliştiriciler İçin)

Projenin kaynak kodlarını Unity Editor üzerinden çalıştırabilir veya kendi Build'inizi alabilirsiniz.

### Gereksinimler
* Unity (Tercihen 2022.3 LTS veya daha güncel bir sürüm)
* Git

### Projeyi Açma
1. Repoyu bilgisayarınıza klonlayın:
   ```bash
   git clone [https://github.com/cevdetozbozkurt/Exiled-Frontiers.git](https://github.com/cevdetozbozkurt/Exiled-Frontiers.git)
   ```
2. Unity Hub'ı açın, `Add -> Add project from disk` diyerek klonladığınız klasörü seçin.
3. Proje açıldığında `Assets/Scenes` klasörüne gidin ve **MainMenu** sahnesini açın.

### Projeyi Derleme (Build Alma)
1. Unity Editor'de üst menüden `File -> Build Settings...` yolunu izleyin.
2. Açılan pencerede **Scenes In Build** kısmında sahnelerin doğru sırayla eklendiğinden emin olun:
   * `0 - MainMenu`
   * `1 - GameScene` *(Kendi oyun sahnenizin adı)*
3. Platform olarak **Windows, Mac, Linux** seçili olduğundan emin olun.
4. **Build** butonuna tıklayın.
5. Bilgisayarınızda tamamen boş ve yeni bir klasör oluşturup bu klasörü seçin.
6. Derleme tamamlandığında, oluşturulan `.exe` dosyasını çalıştırarak oyunu oynayabilirsiniz.

## 🏗️ Mimari Öne Çıkanlar
* **Decoupled Sistemler:** `ResourceManager`, `BuildingManager` ve `UIManager` birbirine bağımlı olmadan Singleton pattern ile global olarak haberleşir.
* **Grid Tabanlı İnşaat:** Binaların yerleşimi serbest değil, görünmez bir grid (ızgara) üzerine oturtularak yapay zekanın (NavMesh) binalar arasına sıkışması engellenmiştir.
* **World Space UI:** İşçilerin üzerindeki roller, kameranın rotasyonunu takip eden (Billboarding) 3D text (TextMeshProUGUI) ile sağlanmıştır.

## 🚀 Gelecek Planları
Zaman kısıtlaması nedeniyle prototip aşamasında bırakılan bu projede, gelecekte şu özelliklerin eklenmesi planlanmaktadır:
* Uygun 3D model (Asset) ve animasyon entegrasyonları.
* Gelişmiş State Machine (FSM) ile daha kompleks işçi davranışları.
* Görsel olarak cilalanmış arayüz (UI) tasarımı ve ses (SFX/BGM) entegrasyonu.