# WalletApp
Aplikacja wykonana w języku C# na zajęcia "Programowanie obiektowe"
Kraków 17.02.2021
## Wymagania formalne
 - [✅] -   baza danych (relacyjna): minimum 4 tabele
-   [✅] - serwer bazodanowy: wskazany SQLight lub MS SQL Server jako LocalDB w VisualStudio (SQL Server Express)
- [✅] -  ORM: Entity Framework dla C#
- [✅] -  interfejs użytkownika: WPF/XAML (absolutnie nie Windows Forms)
-  [✅] - minimum dwa okna (formularze)
-  [✅] - projekt indywidualny (jednoosobowy) prowadzony na GitHub (częste  _commity_)
- [✅] -  dokumentacja XML publicznych składników kodu (zgodnie z wytycznymi Microsoft)
## Wymagania dodatkowe
-  [❌] - testy jednostkowe dla publicznych składników kodu
-  [❌] - instalator aplikacji (Próbował powstać, bezskutecznie)
-  [❓] - dokumentacja aplikacji
## Przedstawienie działania aplikacji

Program wita nas takim o to oknem.
![Główne okno programu](https://i.imgur.com/N2j2znV.png)
Należy wybrać użytkownika, przed przejściem do dodawania naszych przychodów oraz wydatków.

> Przy pierwszym uruchomieniu programu, należy utworzyć użytkownika, wpisując jego pseudonim oraz zatwierdzając przyciskiem "Add user"
> 
![Widok po wybraniu użytkownika](https://i.imgur.com/ougOMWw.png)

-------------------------------------------------------------
**Zakładka "Categories"**
![](https://i.imgur.com/EVDJ5Oj.png)
W tej zakładce możemy dodać, bądź usunąć kategorie, które używane są, do kategoryzowania naszych przychodów i dodatków.
**Zakładka Outgoings**
Zakładka używana jest do wpisywania, usuwania oraz sprawdzania naszych wydatków.
![Widok po wpisaniu przykładowych wydatków](https://i.imgur.com/5Ls6nyt.png)
**Zakładka Incomes**
Zakładka używana jest do wpisywania, usuwania oraz sprawdzania naszych przychodów.
![Widok po wpisaniu przykładowych wydatków](https://i.imgur.com/rgnNWqw.png)

-------------------------------
Widok głównego menu, po wpisaniu powyższych danych.
![enter image description here](https://i.imgur.com/6L1FlMK.png)