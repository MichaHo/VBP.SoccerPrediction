# VBP.SoccerPrediction

# Lastenheft

VBP.SoccerPrediction

Version 0.0.1

Historie der Dokumentversionen

| Version | Datum | Autor | Änderungsgrund / Bemerkungen |
| --- | --- | --- | --- |
| 0.0.1 | 18.02.2020 | Michael Hoffmann | Ersterstellung |

# 1 Einleitung

## 1.1 Allgemeines

### 1.1.1 Zweck und Ziel dieses Dokuments

Dieses Lastenheft beschreibt wie und in welchem Umfang die Aplikation SoccerPrediction erstellt wird, welche Features implementiert werden und wer an diesem Projekt hauptsächlich beteiligt sein wird.

### 1.1.2 Projektbezug

Der User „MichaHo&quot; alias Michael Hoffmann aus dem Forum vb-paradise.de hat den Wunsch geäußert bereits vorab in die Welt der Programmierung von WPF Applikationen unter Einhaltung des MVVM Patterns eingeführt zu werden, es gibt eine Tutorialreihe in diesem Forum welche allerdings noch länger nicht so weit fortgeschritten sein wird das hier das Thema MVVM angeschnitten werden kann. Das Projekt soll alle Vorteile aber auch Nachteile von MVVM aufzeigen und sohin so viele Funktionen welche für dieses Thema relevant sind beinhalten um einfach zu zeigen das sich MVVM auch lohnen kann da hierdurch auch viel Programmierarbeit geteilt und wiederverwendet werden kann.

### 1.1.3 Abkürzungen

MVVM – Model – View – ViewModel

WPF – Windows Presentation Foundation

VBP – VB Paradise Forum

### 1.1.4 Ablage, Gültigkeit und Bezüge zu anderen Dokumenten

Keine

## 1.2 Verteiler und Freigabe

### 1.2.1 Verteiler für dieses Lastenheft

| Rolle / Rollen | Name | Telefon | E-Mail | Bemerkungen |
| --- | --- | --- | --- | --- |
| Projektleiter | Sascha Patschka |   | [patschka.sascha@live.com](mailto:patschka.sascha@live.com) |   |
| 2. Projektleiter | Michael Hoffmann |   |   |   |
| Interessierter Mitabeiter | Florian (florian03) |   |   |   |

## 1.3 Reviewvermerke und Meeting-Protokolle

Noch keine

### 1.3.1 Erstes bis n-tes Review

Noch keines

# 2 Konzept und Rahmenbedingungen

## 2.1 Ziele des Anbieters

Ein korrektes (!!) MVVM Beispiel aufzuzeigen und zeigen, dass man MVVM auch ohne extra Frameworks machen kann (durch z.B. Services=

Gewisse Hauptfunktionen sollte die Applikation können (Siehe Punkt 3) damit diese ein vollständiges Programm darstellt da ich der Meinung bin das es keinen Sinn macht ein MVVM Beispiel zu erstellen welches zwar korrekt aufgebaut ist jedoch völlig sinnfrei ist. Alle implementierten Funktionen müssen Funktionieren und einen Sinn ergeben und nicht einfach wieder ein MVVM Beispiel abgeben welches im Grunde für nichts da ist und keinen Sinn ergibt. Davon gibt es bereits genug im Netz.

## 2.2 Ziele und Nutzen des Anwenders

Ein Funktionierendes Beispiel für eine Anwendung zur Organisation und Ausführung eines Tippspiels und als OpenSource Beispiel zum Lernen.

## 2.3 Benutzer / Zielgruppe

Programmierer

## 2.4 Systemvoraussetzungen

Es sollte alle gängigen Rechner ab Windows 10 32Bit und 64 Bit unterstützt werden.

## 2.5 Ressourcen

[https://materialdesignicons.com/](https://materialdesignicons.com/) - XAML Icons

[https://www.openligadb.de/](https://www.openligadb.de/) - Bundesliga API - mit allen Spielen und wenn fertig mit Ergebnissen

# 3 Beschreibung der Anforderungen

Schnell und einfach zu bedienen - schöne UI!

Grundfunktionen:

- Login für User und Admin
- ev. Register für User anhand eines Token (Admin erstellt Tippgemeinschaft, erhält ein Token und User registriert sich zu dieser Tippgemeinschaft)
- Einstellung verschiedener Vorgaben (wie Punkteregelung beim Tippen)
- User kann Tips erstellen, einsehen, ändern (bis zum Vorgabezeitpunkt), seinen Rang Platz sehen, seine Gesamtpunkte und seine Punkte am Spieltag einsehen
- Admin kann User anlegen, löschen, ändern,sperren, Vorgaben einstellen, Ergebnisse eintragen (eventuell auch abrufen)
- Ranglisten drucken, exportieren (Reportfunktion)
- Admin kann selber auch Tipper sein
- Begegnungen eintragen oder abrufen (mii Hilfe der oben erwähnten API)
- Admin kann Tips für andere User eintragen/ändern
- Vollständige Protokollierung (wenn Admin z.B. einen Tipp ändert)

Unterstützte Endgeräte:

- .Desktop WPF

## 3.1 Anforderung 1

### 3.1.1 Datenspeicherung

Die Datenspeicherung sollte auf jeden Fall mit Hilfe einer Datenbank stattfinden, da hier mehrere Benutzer das Programm verwenden können.
Hier sollte auf EF .Core 3 gesetzt werden.

#### Alternativen (für die Entwicklung)
Für die Entwicklung kann man auch auf eine lokale XML setzen (oder SQLite Datenbank)

# 4 Freigabe / Genehmigung

Ausstehend!

# 5 Anhang / Ressourcen

Keine
