# Secure Board Games Forum & Shop

Projekt został stworzony na potrzeby przemiotu **Bezpieczeństwo Baz Danych**

### Treść zadania

Projektowanym systemem jest mobilna platforma społecznościowo-handlowa zorientowana wokół gier planszowych (dalej zwana platformą społecznościowo-handlową) oraz osobny, przeznaczony dla pracowników system umożliwiający monitorowanie platformy (dalej zwany systemem pracowniczym).

Do platformy społecznościowo-handlowej dostęp jest uzyskiwany poprzez zwykłe łącze internetowe i z użyciem przeglądarki i protokołu HTTPS. Do systemu pracowniczego pracownicy dostają się z użyciem tunelu VPN i przeglądarki.

![image](https://user-images.githubusercontent.com/55836292/130479583-725f26e5-c7ef-4d15-be60-a193735579ee.png)

_Rysunek 1.1 Poglądowa architektura systemu_

## Grupy użytkowników

W projekcie wyróżnione są dwie główne grupy użytkowników: klienci oraz pracownicy. Klienci dzielą się na gości i zarejestrowanych użytkowników, a pracownicy na administratorów oraz moderatorów.

- **Goście** – są to użytkownicy niezalogowani do platformy. Mają dostęp do produktów oraz wątków na forum, jednakże nie mogą interagować z platformą – brak możliwości tworzenia ofert sprzedaży oraz tworzenia postów i wątków na forum.
- **Zarejestrowani użytkownicy** – grupa użytkowników, która posiada pełny dostęp do funkcjonalności forum. Mają możliwość tworzenia nowych wątków na forum, pisania postów, korzystania z czatu, dokonywania zakupów, a także tworzenia własnych ofert z grami.
- **Moderatorzy** to użytkownicy dodatkowego systemu. Ich zadaniem jest monitorowanie wątków i postów na forum, oraz zamieszczanych ofert w celu wyłapywania nadużyć i monitorowania kultury wypowiedzi. Mają uprawnienia do usuwania wątków oraz postów na forum, zdejmowania ofert z platformy handlowej oraz blokowania użytkowników.
- **Administratorzy** to użytkownicy dodatkowego systemu. Mają uprawnienia do tworzenia i usuwania kont pracowników (moderatorów i innych administratorów), a także do tworzenia i usuwania kont klientów platformy.

## Oczekiwania użytkowników

Goście powinni mieć możliwość:

1. Rejestracji
2. Przeglądania produktów dostępnych na stronie
3. Przeglądania dostępnych ofert sprzedaży
4. Tworzenia zamówień złożonych z produktów z różnych ofert]
5. Kupowania
6. Przeglądania forum
7. Zgłaszania wątków/postów/ofert do moderatorów
8. Zgłaszania błędów w systemie do administracji

Zarejestrowani użytkownicy powinni mieć możliwość:

1. Zalogowania
2. Przeglądania produktów dostępnych na stronie
3. Przeglądania dostępnych ofert sprzedaży
4. Tworzenia zamówień złożonych z produktów z różnych ofert
5. Kupowania
6. Zbierania punktów lojalnościowych
7. Korzystania ze zniżek otrzymywanych za punkty lojalnościowe
8. Przeglądania forum
9. Tworzenia wątków na forum
10. Pisania postów pod wątkami na forum
11. Korzystania z czatu
12. Zgłaszania wątków/postów/ofert/wiadomości/użytkowników do moderatorów
13. Zgłaszania błędów w systemie do administracji

Moderatorzy powinni mieć możliwość:

1. Zalogowania
2. Przeglądania forum
3. Usuwania postów i wątków na forum
4. Przeglądania czatu
5. Usuwania wiadomości na czacie
6. Przeglądania ofert
7. Zdejmowania ofert z platformy
8. Przeglądania listy kont użytkowników platformy
9. Blokowania kont użytkowników platformy
10. Otrzymywania przeznaczonych dla siebie powiadomień

Administratorzy powinni mieć możliwość:

1. Zalogowania
2. Przeglądania listy kont użytkowników platformy
3. Blokowania kont użytkowników platformy
4. Zakładania kont pracowniczych
5. Przeglądania listy kont pracowników
6. Blokowania kont pracowników
7. Otrzymywania przeznaczonych dla siebie powiadomień

## Metody dostępu i prezentacji danych

Metodami dostępu do danych mają być dwie aplikacje webowe: platforma społecznościowo-handlowa oraz aplikacja pracownicza.

Platforma społecznościowo-handlowa ma mieć dostęp jedynie do danych znajdujących się w bazie przeznaczonej dla platformy, tzn. forum, czatu oraz produktów i ofert.

![image](https://user-images.githubusercontent.com/55836292/130479607-66b30b0d-2785-4d6e-b715-1e6356a4b793.png)

_Rysunek 1.2 Katalog produktów_

![image](https://user-images.githubusercontent.com/55836292/130479619-70fc8471-f4d8-4010-ba10-f9b7cc7f3933.png)

_Rysunek 1.3 Lista wątków na forum oraz otwarty czat_

![image](https://user-images.githubusercontent.com/55836292/130479655-ff819455-895c-45ed-a009-f82f2eed12c6.png)

_Rysunek 1.4 Wątek na forum_

Aplikacja pracownicza ma posiadać dostęp do danych znajdujących się zarówno w bazie platformy handlowej, jak i osobno wydzielonej bazie przechowującej dane pracowników, ich uprawnienia, powiadomienia skierowane do nich oraz tabele dziennikowe przechowujące logi ich działań.

![image](https://user-images.githubusercontent.com/55836292/130479666-40797ebc-19ed-4e4b-af8e-b2b316c4f0ce.png)

_Rysunek 1.5 Lista użytkowników platformy_

![image](https://user-images.githubusercontent.com/55836292/130479675-15a9eb36-85b9-4b4a-8935-2f1e0621e530.png)

_Rysunek 1.6 Lista pracowników platformy_

# Struktura danych systemu

## Platforma społecznościowo-handlowa

![image](https://user-images.githubusercontent.com/55836292/130479707-2a824b08-8a3c-4df7-9954-e4e19bf0b77a.png)

_Rysunek 2.1 Diagram wygenerowany przez program DBeaver_

## System pracowniczy

![image](https://user-images.githubusercontent.com/55836292/130479727-da91fbb2-8d6d-46ce-bd24-ccd8f68518b7.png)

_Rysunek 2.2 Diagram wygenerowany przez program DBeaver_

# Fizyczna reprezentacja

## Wybór Systemu Zarządzania Relacyjną Bazą Danych

Wybrany RDBMS to PostgreSQL. Został wybrany ze względu na doświadczenie programistów z tym systemem zarządzania oraz sprawdzoną kompatybilność z frameworkiem Entity Framework, który poprzez technologię Fluent Migrator zapewnił migracje bazy danych, a także implementację mapowania obiektowo-relacyjnego (OMR), poprzez którą dokonywano operacji na bazie danych.

## Przygotowanie migracji tworzących bazę

W celu utworzenia struktury bazy danych stworzono zbiór migracji napisanych we frameworku FluentMigrator dla każdego z systemów. W tym celu powstały dwa projekty: BBDProject.Clients.Db oraz BBDProject.Management.Db.

![image](https://user-images.githubusercontent.com/55836292/130479759-f3e78852-aaff-427c-8053-e7241a5a7eae.png)

_Rysunek 3.1 Migracje platformy społecznościowo – hadlowej_

![image](https://user-images.githubusercontent.com/55836292/130479771-71d93233-c736-437c-a9c0-baf5015b9c25.png)

_Rysunek 3.2 Migracje systemu pracowniczego_

![image](https://user-images.githubusercontent.com/55836292/130479786-827e9b47-8cd8-4f3b-b12a-c1fd339b31ed.png)

_Rysunek 3.3 Przykładowa migracja tabeli z ofertami sprzedaży_

# Implementacja systemu

## Rejestracja nowego użytkownika

Rejestracja odbywa się poprzez wypełnienie przez użytkownika odpowiedniego formularza rejestracji. Dane z formularza są następnie wysyłane na serwer w zaszyfrowanej formie w poprzez zapytanie POST. Serwer aplikacji odbiera dane, sprawdza czy użytkownik o podanym mailu lub nazwie użytkownika nie istnieje, a jeśli nie, to hashuje podane hasło i zapisuje je w bazie razem z pozostałymi danymi. Następnie użytkownikowi wysyłany jest mail z łączem do aplikacji zawierającym token uwierzytelniający, dzięki któremu użytkownik może potwierdzić swój adres email. Token jest generowany przez bibliotekę Microsoft Identity.

![image](https://user-images.githubusercontent.com/55836292/130480333-9d7950a7-d16c-40b6-b728-f0e6f5577eb5.png)

_Rysunek 4.1 Formularz rejestracji_

![image](https://user-images.githubusercontent.com/55836292/130480348-9ecc4508-e9dc-4849-aa4b-a4b3ba77cbb7.png)

_Rysunek 4.2 Komunikat powodzenia_

Jeśli użytkownik spróbuje zalogować się posiadając niepotwierdzony adres email, zostanie wyświetlony odpowiedni komunikat.

![image](https://user-images.githubusercontent.com/55836292/130480361-7a5c888f-4a17-424f-9884-411fd6aba97f.png)

_Rysunek 4.3 Komunikat błędu_

## Logowanie

Logowanie użytkownika odbywa się poprzez wypełnienie odpowiedniego formularza logowania na stronie startowej aplikacji. Po wypełnieniu, dane z formularza wysyłane są w zaszyfrowanej formie na serwer poprzez zapytanie POST. Po odebraniu danych serwer aplikacji sprawdza czy istnieje użytkownik o podanym emailu lub nazwie użytkownika. Jeśli tak, to sprawdzane jest hasło użytkownika – podane hasło zostaje odpowiednio shashowane i porównane ze skrótem hasła umieszczonym podczas rejestracji w bazie. Jeśli hasła się zgadzają, to użytkownik zostaje uwierzytelniony i otrzymuje odpowiedni identyfikator sesji.

![image](https://user-images.githubusercontent.com/55836292/130480423-58e76789-1afc-4e81-9f46-2b4f8e77221d.png)

_Rysunek 4.4 Ekran logowania_

Jeśli użytkownik nie zostanie znaleziony lub jego hasło okaże się niepoprawne, strona wyświetli komunikat: „niepoprawna nazwa użytkownika lub hasło&quot;. W przypadku jeśli użytkownik jest zablokowany, to również otrzyma stosowny komunikat.

![image](https://user-images.githubusercontent.com/55836292/130480432-946556b6-e18a-4751-9b15-8e7d2a41e5ae.png)

_Rysunek 4.5 Komunikat błędu logowania_

## Wylogowanie

Wylogowanie odbywa się poprzez wysłania na serwer aplikacyjny żądania wylogowania. Serwer otrzymując takie żądanie kończy sesję użytkownika, dzięki czemu po wysłaniu nieaktywnego tokenu sesji wylogowany użytkownik nie zostanie autoryzowany bez ponownego logowania.

## Przypomnienie hasła

Przypomnienie hasła następuje poprzez wypełnienie odpowiedniego formularza. Po wysłaniu danych z formularza na serwer, użytkownik otrzymuje maila z łączem do aplikacji z tokenem uwierzytelniającym, który pozwala na zresetowanie hasła.

Przypomnienie hasła _per se_ jest niemożliwe, ze względu na to, że w bazie jest ono trzymane w postaci skrótu, który z definicji jest nieodwracalny (w rozsądnym czasie).

![image](https://user-images.githubusercontent.com/55836292/130480474-04d00e38-43ce-4ccc-876f-24ba14d89f05.png)

_Rysunek 4.6 Formularz resetowania hasła_

![image](https://user-images.githubusercontent.com/55836292/130480484-d8060eb8-503d-46a4-8d19-8c550b16e645.png)

_Rysunek 4.7 Formularz zmiany hasła_

## Pulpit administratora

Pulpit administratora daje możliwość wglądu w listę użytkowników oraz pracowników. Administrator ma możliwość blokowania użytkowników oraz pracowników, a także zakładania nowych kont pracowniczych.

![image](https://user-images.githubusercontent.com/55836292/130480498-44e6062a-57ee-4fab-9ac1-2c2e536a6160.png)

_Rysunek 4.8 Ekran zarządzania kontami pracowników_

![image](https://user-images.githubusercontent.com/55836292/130480507-524ca0a5-2b9e-4640-a2e9-99b36e9d0a70.png)

_Rysunek 4.9 Ekran zarządzania kontami użytkowników forum_

Pulpit moderatora daje możliwość zarządzania wątkami i postami pod nimi.

![image](https://user-images.githubusercontent.com/55836292/130480515-b197b07d-6d0f-411c-9e50-ea0ea6ea7b30.png)

_Rysunek 4.10 Ekran zarządzania wątkami na forum_

![image](https://user-images.githubusercontent.com/55836292/130480532-819240c6-a70d-44c6-96f7-245d657730e6.png)

_Rysunek 4.11 Ekran zarządzania postami pod wątkiem_

Usuwanie wątków oraz postów nie oznacza usunięcia rekordu z bazy danych – rekord zostaje oznaczony jako usunięty i pozostaje niewidoczny dla użytkowników, jednakże zostaje zatrzymany w bazie.

## Wybrane funkcjonalności operacyjne systemu

### Czat

Zaimplementowano czat z wykorzystaniem technologii SignalR, która bazuje na technologii websocketów. Klient wprowadza wiadomość na czacie, która jest wysyłana na serwer, który zapisuje ją w bazie. Następnie serwer wysyła komunikat z informacją o pojawieniu się nowych wiadomości w systemie. Każdy klient, który odebrał nadany komunikat wysyła żądanie do systemu z id ostatniej odebranej wiadomości, na które serwer odpowiada listą wszystkich wiadomości, które pojawiły się od tego czasu.

![image](https://user-images.githubusercontent.com/55836292/130480583-f943f179-d867-48c3-92a0-839dd5485d22.png)

Dostęp do czatu dostępny jest jedynie dla autoryzowanych użytkowników, a dodatkowo wprowadzony system uniemożliwia przejęcie połączenia SignalR i podsłuchanie wiadomości, ponieważ nie są one wysyłane tą drogą.

### Forum

Zaimplementowane zostało forum, które dzieli się na wątki i posty pod wątkami. Zarejestrowani użytkownicy mają możliwość przeglądania forum, oraz tworzenia nowych wątków oraz postów pod wątkami.

![image](https://user-images.githubusercontent.com/55836292/130480602-3c458c24-c833-40ef-a4a4-aeea6dafcb88.png)

_Rysunek 4.12 Strona główna forum_

![image](https://user-images.githubusercontent.com/55836292/130480610-0fab6789-14f1-48d9-99a5-34b5209183b4.png)

_Rysunek 4.13 Formularz tworzenia nowego wątku_

![image](https://user-images.githubusercontent.com/55836292/130480627-eeda5265-0160-44b0-8299-fc33e75f5b07.png)

_Rysunek 4.14 Widok wątku, postów pod nim oraz formularza tworzenia nowego posta_

Klienci niezarejestrowani (goście) mogą forum jedyne przeglądać, bez możliwości interakcji z nim – tworzenia wątków i postów (brak przycisku „utwórz wątek&quot; oraz pola do tworzenia nowego posta).

![image](https://user-images.githubusercontent.com/55836292/130480661-038b0a55-4fdd-41dd-900d-6b1fb534e85f.png)

_Rysunek 4.15 Widok strony głównej forum z perspektywy gościa_

![image](https://user-images.githubusercontent.com/55836292/130480675-c3125b8a-1b33-4e4d-aacc-e5eb14dee7d5.png)

_Rysunek 4.16 Widok wątku na forum z perspektywy gościa_

### Produkty

Zaimplementowana została lista produktów z możliwością przeszukiwania jej.

![image](https://user-images.githubusercontent.com/55836292/130480692-890b9a6a-0df2-40f2-8664-715b7950d411.png)

_Rysunek 4.17 Lista produktów_

### Rejestrowanie czynności pracowniczych

Ze względów bezpieczeństwa zaimplementowano mechanizm logowania czynności wykonywanych przez pracowników. Została utworzona tabela słownikowa _activity_ zawierająca nazwę czynności oraz nazwę tabeli do której dana czynność się odnosi, a także tabela łącząca użytkowników (_employee\_activity_) z wykonanymi czynnościami oraz konkretnymi rekordami z tabel dopasowanych do czynności.

![image](https://user-images.githubusercontent.com/55836292/130480709-94c420d8-f971-4a6c-85bb-dff7ae0135d3.png)

_Rysunek 4.18 Tabela słownikowa z czynnościami_

![image](https://user-images.githubusercontent.com/55836292/130480722-66e2949b-da38-4fbf-854a-581a047a2d7c.png)

_Rysunek 4.19 Tabela rejestrująca czynności pracowników_

# Elementy bezpieczeństwa

## Walidacja danych

Walidacja danych następuje w aplikacji na dwóch poziomach: interfejsu użytkownika oraz logiki biznesowej. Walidowane są każde dane, jakie może wprowadzić użytkownik.

Dane po wprowadzeniu są walidowane z poziomu kodu javascriptowego, a wszystkie nielegalne znaki i instrukcje (jak np. tagi skryptowe HTML razem z kodem) w odpowiedni sposób unieszkodliwiane.

Następnie, po przesłaniu na serwer, dane ponownie są walidowane oraz unieszkodliwiane ze względu na to, że atakujący z łatwością może ominąć kod javascript w przeglądarce i wysłać odpowiednio spreparowane zapytania bezpośrednio na serwer.

Dopiero po walidacji dane mogą zostać wprowadzone do bazy.

## Format przechowywanych danych

Dane są przechowywane w formacie najlepiej odpowiadającym ich typowi. Teksty i napisy trzymane są w type _varchar_, kwoty jako typ zmiennoprzecinkowy, liczby jako całkowito liczbowy (liczby, który potrzebują zachować swój pierwotny format, jak np. numery telefonów, są przechowywane jako tekst), a obrazki jako tablice bajtów.

## Zarządzanie hasłami

Hasła wysyłane są na serwer poprzez zapytanie POST z wykorzystaniem protokołu HTTPS, a więc w formie zaszyfrowanej, uniemożliwiającej potencjalnemu atakującemu podejrzenie wprowadzanych danych. Hasła na serwerze są od razu hashowane, aby uniknąć przechowywania ich w formie jawnej, czytelnej dla dowolnej osoby (również pracowników). W bazie skróty haseł przechowywane są w kodowaniu BASE64.

![image](https://user-images.githubusercontent.com/55836292/130480755-a4e8fb10-c841-4313-9887-798eb5bd495e.png)

_Rysunek 5.1 hash hasła administratora_

## Uprawnienia dla grup użytkowników

Uprawnienia użytkowników zostały zaimplementowane w postaci tzw. ról. Każdy użytkownik ma przypisaną w bazie odpowiednią rolę, której istnienie można egzekwować przy wykonywaniu różnorakich czynności w systemie.

Rola użytkownika sprawdzana jest w dwóch miejscach: odbierając żądanie oraz wykonując czynność. W pierwszym przypadku system sprawdza, czy wysyłający ją użytkownik jest przypisany do odpowiedniej roli; w drugim rola jest sprawdzana w metodach logiki biznesowej, aby nie było możliwe wywołanie metody wyższego poziomu przez zapytanie niższego.

## Integralność danych

Integralność danych zapewniania jest poprzez transakcje otwierane na bazie relacyjnej. Dopóki transakcje nie zostanie zatwierdzona wszystkie dane pozostają w niezmienionej formie w bazie.

Aby zapewnić integralność danych historycznych wprowadzono zasadę nieusuwania danych. Dane, które mają zostać usunięte zmieniają wartość flagi _deleted_ z **false** na **true** i pozostają niewidoczne dla użytkownika, jednakże zostają zatrzymane w bazie ze względów integralności i spójności danych.

## Wydajność

Wydajność bazy danych może zostać zwiększona poprzez utworzenie odpowiednich indeksów oraz jak najbardziej przemyślane i ograniczone wykonywanie zapytań na bazie.

Dane usunięte oraz dane z dzienników czynności mogą być kopiowane do zewnętrznej bazy dla trzymania ciągłości danych i z powodów audytowych, jednakże usuwane z bazy produkcyjnej dla zwiększenia jej prędkości operacyjnej.

## Backup

Backup bazy danych PostgreSQL można zapewnić poprzez wykorzystanie narzędzi pg\_dump oraz utworzenie odpowiednich zadań cyklicznych w cronie.

## Bezpieczeństwo aplikacji

Bezpieczeństwo aplikacji zapewnione jest poprzez rozdzielenie bazy pracowników od bazy klientów platformy. Dzięki temu jeśli potencjalnemu agresorowi uda się uzyskać dostęp do bazy klienckiej, to niemożliwe będzie pozyskanie również danych pracowników dostępnych jedynie z poziomu aplikacji pracowniczej.

Dodatkowo poziom bezpieczeństwa zostaje zwiększony poprzez wymuszenie protokołu HTTPS. Po otrzymaniu zapytania nieszyfrowanego obie aplikacje (kliencka i pracownicza) przekierowują połączenie na protokół HTPS.

## Inne elementy zaproponowane przez studenta

### Blokowanie użytkowników

Zaimplementowano mechanizm zliczania logowań użytkownika, który blokuje konto po osiągnięciu ustalonej wcześniej granicy. Konto za pierwszym razem jest blokowane na godzinę po dziesięciu próbach, następnie po pięciu, a w końcu po dwóch zostaje zablokowane na stałe (lub do interwencji administratora).

![image](https://user-images.githubusercontent.com/55836292/130480791-f066840e-08b0-4c37-a6f6-70777a0fa2f9.png)

_Rysunek 5.2 Komunikat o zablokowanym koncie użytkownika_

### Rejestrowanie czynności pracowniczych

Aby zawsze była możliwość sprawdzenia czynności dokonanych przez pracowników, w celu zwiększenia odpowiedzialności za swoje czyny, wszystkie akcje wykonywane przez pracowników są logowane do tabeli _employee\_activity_, która posiada klucze obce wskazujące na tabelę słownikową _activity_ oraz na rekord, do którego w danej tabeli dana aktywność się odnosi. Dzięki temu w każdej chwili można potwierdzić i sprawdzić czynność wykonaną przez danego pracownika.

![image](https://user-images.githubusercontent.com/55836292/130480836-3f36dc94-0408-406d-b80b-662318c0d258.png)

_Rysunek 5.3 Tabela słownikowa z czynnościami_

![image](https://user-images.githubusercontent.com/55836292/130480848-ed57c7ed-e0aa-402f-a89b-add49eb42942.png)

_Rysunek 5.4 Tabela rejestrująca czynności pracowników_

### Zmiana zwyczajowych nazw tabel

Aby utrudnić potencjalnemu atakującemu eksploatację bazy danych (w razie uzyskania dostępu) zmieniono zwyczajowe nazwy tabel (taki jak _user, employee, role,_ etc.) na mniej powszechne. Aby uzyskać ten efekt zaimplementowano możliwość konfiguracji przedrostków jakie zostają dodane podczas migracji bazy, tak aby każdy z klientów zamawiających powstały system mógł posiadać własne, unikalne nazwy tabel, utrudniając tym samym „ślepą&quot; eksploatację.

![image](https://user-images.githubusercontent.com/55836292/130480872-8fae1c69-993e-4d0e-90bb-185ff3f7fb41.png)

_Rysunek 5.5 Fragment migracji tworzącej tabelę oraz schemat jako nazwy używając tekstów wprowadzonych w konfiguracji aplikacji._
