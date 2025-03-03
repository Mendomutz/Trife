[Setup]
AppName=Trife
AppVersion=1.1.0.2-alpha
DefaultDirName={commonpf}\Trife
DefaultGroupName=Trife
OutputDir=C:\Projetos\Avalonia\Trife\Installer
OutputBaseFilename=TrifeInstaller
Compression=lzma
SolidCompression=yes

[Files]
Source: "C:\Projetos\Avalonia\Trife\Output\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\Trife Desktop"; Filename: "{app}\Trife.Desktop.exe"
Name: "{group}\Uninstall Trife Desktop"; Filename: "{uninstallexe}"