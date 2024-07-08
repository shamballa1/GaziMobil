# Gazi Mobil

BM102 Programlama 2 dersimizin bahar döneminde verilen proje ödevinin reposudur.

Gazi Üniversitesi Mühendislik Fakültesi Bilgisayar Mühendisliği bölüm dersi olan BM102 dersi için 024 yılı bahar dönemi kapsamında verilmiş proje ödevimizdir. Projemizin adı GaziMobil. Uygulamamızda nesne yönelimli bir dil olan C# yazılım dili tabanlı .NET MAUI uygulaması ve uygulama arayüzü için de XAML dilini kullandık.

Bu projenin orataya çıkış nedeni diğer diğer birçok üniversitenin kendi mobil uygulamasının olması ve Gazi Üniversitesi'nin bir mobil uygulamasının olmamasıdır. Uygulamamızın içeriğinde Gazi Üüniversitesi mekez kampüs haritası, üniversitemizin web sayfasında duyurulann duyurular sayfası, yemekhanede çıkacak olan yemeklerin listesi, kendi ders programımızı oluşturmak için ders programı sayfası ve öğrenciler için genel ve dönemsel not ortalaması hesaplama sayfası bulunmaktadır. Böylelikle üniversitemiz bünyesinde bulunan herkes için kullanışlı bir uygulama ortaya çıkarmaya çalıştık. 

Uygulamamızı ilk açtığımızda karşımıza hava durumu, o gün yemekhanede çıkacak olan günün menüsü ve üniversitemizin websitesinden duyurulan duyurular kısmı bulunmaktadır. Ayrıca ekranın sol üstünde bulunan menü tuşundan uygulamamızın diğer özelliklerine erişebiliyoruz. Buradan diğer sayfalara geçiş apabiliyoruz.

Sol üstte bulunan menü sayfasına tıkladığımzıda karşımıza 8 satırlık bir menü çıkıyor:  
1. İlk olarak karşımıza Ana Sayfa butonu çıkıyor. Bu buton bizi uygulama ilk açıldığında karşımıza çıkan sayfaya götürüyor.
2. İkinci olarak Harita sayfamız var. Burada üniversitemizn merkez kampüsünün haritası bulunmakta.
3. Üçüncü olarak Kütüphane sayfamız var. Burada merkez kütüphanemizin çalışma saatlerini görebiliyoruz. Ayrıca altta bulunan kitapa arama butonuna tıklarsak kütüphanemizin websitesine gidiyor ve kütüphane veri tabanına erişebiliyoruz.
4. Dördüncü olarak Akademik takvim sayfamız var. Burada üniversitemizin o yıl yayımladığı akademik takvimi görebiliyoruz.
5. Beşinci olarak Duyrular sayfamız var. Burada https://gazi.edu.tr sayfasından "web scrapting" metodunu kullanarak edindiğimiz verileri kendi sayfamızda görüntüleyebiliyoruz.
6.Altıncı olarak Yemekhane Listesi sayfamız bulunmaktadır. Üniversitemizin https://mediko.gazi.edu.tr/view/page/20412 sayfasında yayımlanan yemek listesini yine "web scrapting" metoduyla kendi sayfamıza ekledik. Ayrıca burdan aldığımız verileri anasyfamızda bulunan yemek listesi kısmında o günün menüsünü görebiliyoruz.  
7. Yedinci olarak Not Hesaplama sayfamız var. Burada hem dönem ortalaması hem de genel not ortalaması hesaplayan bir algoritmamız var. Öncelikle hangi ortalamayı hesaplamak istediğini kullanıcıya soruyoruz. Sonrasında ders adını girmesini bekliyoruz. Ardından o dersin AKTS'sini girmesini ve sonrasında da aldığı harf notunu giriyoruz. Bu bilgiler girildikten sonra sayfanın altında girdiğimiz bilgiler sırasıyla gösteriliyor ardından da ortalamamızı görüyoruz.
8. Sekizinci olarak ise Ders Programı sayfamız var. Burada kullanıcın kendi ders prograını oluşturmasını bekliyoruz. Bu sayfayı açtığımda örnek bir program gözükmekte. Bu program biz kendi dersimizi ekranın sağ altında bulunan artı(+) tuşuna bastıktan ve kendi dersimizi ekledikten sonra kendi ders programını görebilmektedir.

Özetle, bu projemizde üniversite hayatını kolaylaştıracak birtakım özellikler ekleyip üniversitemizde bulunan herkese faydalı olmayı amaçladık.

Projeyi hazırlayanlar:
İbrahim Kamil Şahin
Ahmet Talha Akbul
Taha Emre Orhan
Ahmet Balkan
Bedirhan Yiğit
