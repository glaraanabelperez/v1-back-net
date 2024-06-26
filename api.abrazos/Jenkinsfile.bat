@echo off

REM Servidor FTP y credenciales
set ftp_server=179.61.12.164
set ftp_user=abrazoap
set ftp_pass=!Abrazo30305

REM Directorios locales de los archivos DLL
setlocal enabledelayedexpansion
set local_dirs="C:\ProgramData\Jenkins\.jenkins\workspace\abrazo_deploy\Models\bin\Debug\net6.0" 
"C:\ProgramData\Jenkins\.jenkins\workspace\abrazo_deploy\api.abrazos\bin\Debug\net6.0" 
"C:\ProgramData\Jenkins\.jenkins\workspace\abrazo_deploy\ServiceEventHandler\bin\Debug\net6.0" 
"C:\ProgramData\Jenkins\.jenkins\workspace\abrazo_deploy\Services\bin\Debug\net6.0" 
"C:\ProgramData\Jenkins\.jenkins\workspace\abrazo_deploy\Utils\bin\Debug\net6.0"

REM Directorio remoto en el servidor FTP
set remote_dir=/httpdocs

REM Limpiar archivos temporales si existen previamente
if exist ftp_commands.txt del ftp_commands.txt

REM Transferir archivos DLL y PDB de cada directorio local
for %%d in (%local_dirs%) do (
    for %%f in (%%d\*.dll %%d\*.pdb %%d\*.json %%d\*.exe) do (
        REM Crear archivo temporal para comandos FTP
        (
            echo open %ftp_server%
            echo USER %ftp_user%
            echo PASS %ftp_pass%
            echo bin
            echo cd %remote_dir%
            echo put "%%f"
            echo bye
        ) > ftp_commands.txt
        REM Ejecutar los comandos FTP
        ftp -n -s:ftp_commands.txt

        REM Esperar 10 segundos antes de continuar con el siguiente archivo
        timeout /t 10 /nobreak
    )
)

REM Limpiar archivo temporal después de la transferencia
if exist ftp_commands.txt del ftp_commands.txt

echo Transferencia de archivos completada.
