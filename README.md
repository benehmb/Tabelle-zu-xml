# Tabelle zu XML

[TOC]

## General

Dieses Programm wandelt nicht jede Beliebige Tabelle zu einer XML um. Es ist lediglich für ein bestimmtes Inventor-Plugin gedacht: <name einfügen>

## Verwendung

Das Programm erwartet eine Settings.xml in seinem Root-Verzeichniss. Hier ist eine Beispiel Settings-Datei:

### Beispiel Settings-Datei

```xml
<?xml version="1.0" encoding="utf-8"?>
<Settings>
	<Delimiter>;</Delimiter>
	<FavoritString>Senkung Zylinderkopf ISO 4762</FavoritString>
	<OutputFile>Senkungenoutput.xml</OutputFile>
	<FallbackDefaultValue>n.v.</FallbackDefaultValue>
	<Values>
		<Value>Favorit</Value>
		<Value>Bohrungstyp</Value>
		<Value>Ø</Value>
		<Value>Ø_x0020_Tol.</Value>
		<Value>Flach</Value>
		<Value>Ausführungstyp</Value>
		<Value>Abstand</Value>
		<Value>Abst._x0020_Tol.</Value>
		<Value>Ø_2</Value>
		<Value>Ø_2_x0020_Tol.</Value>
		<Value>Tiefe</Value>
		<Value>Tiefe_x0020_Tol.</Value>
		<Value>Winkel</Value>
		<Value>Gewindebez</Value>
		<Value>Gewindetyp</Value>
		<Value>Norm</Value>
		<Value>Klasse</Value>
		<Value>volle_x0020_Tiefe</Value>
		<Value>Gewindetiefe</Value>
	</Values>
	<DefaultValues>
		<DefaultValue name="Bohrungstyp">Zapfen</DefaultValue>
		<DefaultValue name="Flach">ja</DefaultValue>
		<DefaultValue name="Ø_x0020_Tol.">H13(E)</DefaultValue>
		<DefaultValue name="Ausführungstyp">Durch</DefaultValue>
		<DefaultValue name="Abstand">Durch</DefaultValue>
		<DefaultValue name="Ø_2_x0020_Tol.">H13(E)</DefaultValue>
		<DefaultValue name="Tiefe_x0020_Tol.>">0,01 mm/0 mm(B)</DefaultValue>
	</DefaultValues>
</Settings>
```

Zu dieser Settings-Date erwartet das Programm eine Vorlagendatei, wie diese hier (oder eine umgewandelte Excel-Tabelle):

###  Beispiel CSV-Datei

```csv
M;3;4;5;6;8;10;12;16;20;24;30;36
Ø;3,4;4,5;5,5;6,6;9;11;13,5;17,5;22;26;33;39
Ø_2;6,5;8;10;11;15;18;20;26;33;40;50;58
Tiefe;3,4;4,4;5,4;6,4;8,6;10,6;12,6;16,6;20,6;24,8;31;37
```

Anhand dieser zwei Dateien, erstellt es dann eine Datei, welche von <Plugin einfügen> verwendet werden kann.

Hier ein Beispiel einer fertigen Ausgabe-Datei:

### Beispiel Ausgabe-Datei

```xml
<?xml version="1.0" encoding="UTF-8"?>
<DocumentElement>
   <Favoriten>
      <Favorit>Senkung Zylinderkopf ISO 4762 M3</Favorit>
      <Bohrungstyp>Zapfen</Bohrungstyp>
      <Ø>3,4</Ø>
      <Ø_x0020_Tol.>H13(E)</Ø_x0020_Tol.>
      <Flach>ja</Flach>
      <Ausführungstyp>Durch</Ausführungstyp>
      <Abstand>Durch</Abstand>
      <Abst._x0020_Tol.>n.v.</Abst._x0020_Tol.>
      <Ø_2>6,5</Ø_2>
      <Ø_2_x0020_Tol.>H13(E)</Ø_2_x0020_Tol.>
      <Tiefe>3,4</Tiefe>
      <Tiefe_x0020_Tol.>n.v.</Tiefe_x0020_Tol.>
      <Winkel>n.v.</Winkel>
      <Gewindebez>n.v.</Gewindebez>
      <Gewindetyp>n.v.</Gewindetyp>
      <Norm>n.v.</Norm>
      <Klasse>n.v.</Klasse>
      <volle_x0020_Tiefe>n.v.</volle_x0020_Tiefe>
      <Gewindetiefe>n.v.</Gewindetiefe>
   </Favoriten>
   <Favoriten>
      <Favorit>Senkung Zylinderkopf ISO 4762 M4</Favorit>
      <Bohrungstyp>Zapfen</Bohrungstyp>
      <Ø>4,5</Ø>
      <Ø_x0020_Tol.>H13(E)</Ø_x0020_Tol.>
      <Flach>ja</Flach>
      <Ausführungstyp>Durch</Ausführungstyp>
      <Abstand>Durch</Abstand>
      <Abst._x0020_Tol.>n.v.</Abst._x0020_Tol.>
      <Ø_2>8</Ø_2>
      <Ø_2_x0020_Tol.>H13(E)</Ø_2_x0020_Tol.>
      <Tiefe>4,4</Tiefe>
      <Tiefe_x0020_Tol.>n.v.</Tiefe_x0020_Tol.>
      <Winkel>n.v.</Winkel>
      <Gewindebez>n.v.</Gewindebez>
      <Gewindetyp>n.v.</Gewindetyp>
      <Norm>n.v.</Norm>
      <Klasse>n.v.</Klasse>
      <volle_x0020_Tiefe>n.v.</volle_x0020_Tiefe>
      <Gewindetiefe>n.v.</Gewindetiefe>
   </Favoriten>
   <Favoriten>
      <Favorit>Senkung Zylinderkopf ISO 4762 M5</Favorit>
      <Bohrungstyp>Zapfen</Bohrungstyp>
      <Ø>5,5</Ø>
      <Ø_x0020_Tol.>H13(E)</Ø_x0020_Tol.>
      <Flach>ja</Flach>
      <Ausführungstyp>Durch</Ausführungstyp>
      <Abstand>Durch</Abstand>
      <Abst._x0020_Tol.>n.v.</Abst._x0020_Tol.>
      <Ø_2>10</Ø_2>
      <Ø_2_x0020_Tol.>H13(E)</Ø_2_x0020_Tol.>
      <Tiefe>5,4</Tiefe>
      <Tiefe_x0020_Tol.>n.v.</Tiefe_x0020_Tol.>
      <Winkel>n.v.</Winkel>
      <Gewindebez>n.v.</Gewindebez>
      <Gewindetyp>n.v.</Gewindetyp>
      <Norm>n.v.</Norm>
      <Klasse>n.v.</Klasse>
      <volle_x0020_Tiefe>n.v.</volle_x0020_Tiefe>
      <Gewindetiefe>n.v.</Gewindetiefe>
   </Favoriten>
   <Favoriten>
      <Favorit>Senkung Zylinderkopf ISO 4762 M6</Favorit>
      <Bohrungstyp>Zapfen</Bohrungstyp>
      <Ø>6,6</Ø>
      <Ø_x0020_Tol.>H13(E)</Ø_x0020_Tol.>
      <Flach>ja</Flach>
      <Ausführungstyp>Durch</Ausführungstyp>
      <Abstand>Durch</Abstand>
      <Abst._x0020_Tol.>n.v.</Abst._x0020_Tol.>
      <Ø_2>11</Ø_2>
      <Ø_2_x0020_Tol.>H13(E)</Ø_2_x0020_Tol.>
      <Tiefe>6,4</Tiefe>
      <Tiefe_x0020_Tol.>n.v.</Tiefe_x0020_Tol.>
      <Winkel>n.v.</Winkel>
      <Gewindebez>n.v.</Gewindebez>
      <Gewindetyp>n.v.</Gewindetyp>
      <Norm>n.v.</Norm>
      <Klasse>n.v.</Klasse>
      <volle_x0020_Tiefe>n.v.</volle_x0020_Tiefe>
      <Gewindetiefe>n.v.</Gewindetiefe>
   </Favoriten>
   <Favoriten>
      <Favorit>Senkung Zylinderkopf ISO 4762 M8</Favorit>
      <Bohrungstyp>Zapfen</Bohrungstyp>
      <Ø>9</Ø>
      <Ø_x0020_Tol.>H13(E)</Ø_x0020_Tol.>
      <Flach>ja</Flach>
      <Ausführungstyp>Durch</Ausführungstyp>
      <Abstand>Durch</Abstand>
      <Abst._x0020_Tol.>n.v.</Abst._x0020_Tol.>
      <Ø_2>15</Ø_2>
      <Ø_2_x0020_Tol.>H13(E)</Ø_2_x0020_Tol.>
      <Tiefe>8,6</Tiefe>
      <Tiefe_x0020_Tol.>n.v.</Tiefe_x0020_Tol.>
      <Winkel>n.v.</Winkel>
      <Gewindebez>n.v.</Gewindebez>
      <Gewindetyp>n.v.</Gewindetyp>
      <Norm>n.v.</Norm>
      <Klasse>n.v.</Klasse>
      <volle_x0020_Tiefe>n.v.</volle_x0020_Tiefe>
      <Gewindetiefe>n.v.</Gewindetiefe>
   </Favoriten>
   <Favoriten>
      <Favorit>Senkung Zylinderkopf ISO 4762 M10</Favorit>
      <Bohrungstyp>Zapfen</Bohrungstyp>
      <Ø>11</Ø>
      <Ø_x0020_Tol.>H13(E)</Ø_x0020_Tol.>
      <Flach>ja</Flach>
      <Ausführungstyp>Durch</Ausführungstyp>
      <Abstand>Durch</Abstand>
      <Abst._x0020_Tol.>n.v.</Abst._x0020_Tol.>
      <Ø_2>18</Ø_2>
      <Ø_2_x0020_Tol.>H13(E)</Ø_2_x0020_Tol.>
      <Tiefe>10,6</Tiefe>
      <Tiefe_x0020_Tol.>n.v.</Tiefe_x0020_Tol.>
      <Winkel>n.v.</Winkel>
      <Gewindebez>n.v.</Gewindebez>
      <Gewindetyp>n.v.</Gewindetyp>
      <Norm>n.v.</Norm>
      <Klasse>n.v.</Klasse>
      <volle_x0020_Tiefe>n.v.</volle_x0020_Tiefe>
      <Gewindetiefe>n.v.</Gewindetiefe>
   </Favoriten>
   <Favoriten>
      <Favorit>Senkung Zylinderkopf ISO 4762 M12</Favorit>
      <Bohrungstyp>Zapfen</Bohrungstyp>
      <Ø>13,5</Ø>
      <Ø_x0020_Tol.>H13(E)</Ø_x0020_Tol.>
      <Flach>ja</Flach>
      <Ausführungstyp>Durch</Ausführungstyp>
      <Abstand>Durch</Abstand>
      <Abst._x0020_Tol.>n.v.</Abst._x0020_Tol.>
      <Ø_2>20</Ø_2>
      <Ø_2_x0020_Tol.>H13(E)</Ø_2_x0020_Tol.>
      <Tiefe>12,6</Tiefe>
      <Tiefe_x0020_Tol.>n.v.</Tiefe_x0020_Tol.>
      <Winkel>n.v.</Winkel>
      <Gewindebez>n.v.</Gewindebez>
      <Gewindetyp>n.v.</Gewindetyp>
      <Norm>n.v.</Norm>
      <Klasse>n.v.</Klasse>
      <volle_x0020_Tiefe>n.v.</volle_x0020_Tiefe>
      <Gewindetiefe>n.v.</Gewindetiefe>
   </Favoriten>
   <Favoriten>
      <Favorit>Senkung Zylinderkopf ISO 4762 M16</Favorit>
      <Bohrungstyp>Zapfen</Bohrungstyp>
      <Ø>17,5</Ø>
      <Ø_x0020_Tol.>H13(E)</Ø_x0020_Tol.>
      <Flach>ja</Flach>
      <Ausführungstyp>Durch</Ausführungstyp>
      <Abstand>Durch</Abstand>
      <Abst._x0020_Tol.>n.v.</Abst._x0020_Tol.>
      <Ø_2>26</Ø_2>
      <Ø_2_x0020_Tol.>H13(E)</Ø_2_x0020_Tol.>
      <Tiefe>16,6</Tiefe>
      <Tiefe_x0020_Tol.>n.v.</Tiefe_x0020_Tol.>
      <Winkel>n.v.</Winkel>
      <Gewindebez>n.v.</Gewindebez>
      <Gewindetyp>n.v.</Gewindetyp>
      <Norm>n.v.</Norm>
      <Klasse>n.v.</Klasse>
      <volle_x0020_Tiefe>n.v.</volle_x0020_Tiefe>
      <Gewindetiefe>n.v.</Gewindetiefe>
   </Favoriten>
   <Favoriten>
      <Favorit>Senkung Zylinderkopf ISO 4762 M20</Favorit>
      <Bohrungstyp>Zapfen</Bohrungstyp>
      <Ø>22</Ø>
      <Ø_x0020_Tol.>H13(E)</Ø_x0020_Tol.>
      <Flach>ja</Flach>
      <Ausführungstyp>Durch</Ausführungstyp>
      <Abstand>Durch</Abstand>
      <Abst._x0020_Tol.>n.v.</Abst._x0020_Tol.>
      <Ø_2>33</Ø_2>
      <Ø_2_x0020_Tol.>H13(E)</Ø_2_x0020_Tol.>
      <Tiefe>20,6</Tiefe>
      <Tiefe_x0020_Tol.>n.v.</Tiefe_x0020_Tol.>
      <Winkel>n.v.</Winkel>
      <Gewindebez>n.v.</Gewindebez>
      <Gewindetyp>n.v.</Gewindetyp>
      <Norm>n.v.</Norm>
      <Klasse>n.v.</Klasse>
      <volle_x0020_Tiefe>n.v.</volle_x0020_Tiefe>
      <Gewindetiefe>n.v.</Gewindetiefe>
   </Favoriten>
   <Favoriten>
      <Favorit>Senkung Zylinderkopf ISO 4762 M24</Favorit>
      <Bohrungstyp>Zapfen</Bohrungstyp>
      <Ø>26</Ø>
      <Ø_x0020_Tol.>H13(E)</Ø_x0020_Tol.>
      <Flach>ja</Flach>
      <Ausführungstyp>Durch</Ausführungstyp>
      <Abstand>Durch</Abstand>
      <Abst._x0020_Tol.>n.v.</Abst._x0020_Tol.>
      <Ø_2>40</Ø_2>
      <Ø_2_x0020_Tol.>H13(E)</Ø_2_x0020_Tol.>
      <Tiefe>24,8</Tiefe>
      <Tiefe_x0020_Tol.>n.v.</Tiefe_x0020_Tol.>
      <Winkel>n.v.</Winkel>
      <Gewindebez>n.v.</Gewindebez>
      <Gewindetyp>n.v.</Gewindetyp>
      <Norm>n.v.</Norm>
      <Klasse>n.v.</Klasse>
      <volle_x0020_Tiefe>n.v.</volle_x0020_Tiefe>
      <Gewindetiefe>n.v.</Gewindetiefe>
   </Favoriten>
   <Favoriten>
      <Favorit>Senkung Zylinderkopf ISO 4762 M30</Favorit>
      <Bohrungstyp>Zapfen</Bohrungstyp>
      <Ø>33</Ø>
      <Ø_x0020_Tol.>H13(E)</Ø_x0020_Tol.>
      <Flach>ja</Flach>
      <Ausführungstyp>Durch</Ausführungstyp>
      <Abstand>Durch</Abstand>
      <Abst._x0020_Tol.>n.v.</Abst._x0020_Tol.>
      <Ø_2>50</Ø_2>
      <Ø_2_x0020_Tol.>H13(E)</Ø_2_x0020_Tol.>
      <Tiefe>31</Tiefe>
      <Tiefe_x0020_Tol.>n.v.</Tiefe_x0020_Tol.>
      <Winkel>n.v.</Winkel>
      <Gewindebez>n.v.</Gewindebez>
      <Gewindetyp>n.v.</Gewindetyp>
      <Norm>n.v.</Norm>
      <Klasse>n.v.</Klasse>
      <volle_x0020_Tiefe>n.v.</volle_x0020_Tiefe>
      <Gewindetiefe>n.v.</Gewindetiefe>
   </Favoriten>
   <Favoriten>
      <Favorit>Senkung Zylinderkopf ISO 4762 M36</Favorit>
      <Bohrungstyp>Zapfen</Bohrungstyp>
      <Ø>39</Ø>
      <Ø_x0020_Tol.>H13(E)</Ø_x0020_Tol.>
      <Flach>ja</Flach>
      <Ausführungstyp>Durch</Ausführungstyp>
      <Abstand>Durch</Abstand>
      <Abst._x0020_Tol.>n.v.</Abst._x0020_Tol.>
      <Ø_2>58</Ø_2>
      <Ø_2_x0020_Tol.>H13(E)</Ø_2_x0020_Tol.>
      <Tiefe>37</Tiefe>
      <Tiefe_x0020_Tol.>n.v.</Tiefe_x0020_Tol.>
      <Winkel>n.v.</Winkel>
      <Gewindebez>n.v.</Gewindebez>
      <Gewindetyp>n.v.</Gewindetyp>
      <Norm>n.v.</Norm>
      <Klasse>n.v.</Klasse>
      <volle_x0020_Tiefe>n.v.</volle_x0020_Tiefe>
      <Gewindetiefe>n.v.</Gewindetiefe>
   </Favoriten>
</DocumentElement>
```

## Settings

## Spezialfälle

### M

### Favoriten

## Consolenverwendung

### `-f <datei>`-Attribut

### `-o <datei/verzeichnis`-Attribut