if not exist "%APPDATA%\JetBrains\ReSharper\v4.0\vs9.0\Plugins" mkdir "%APPDATA%\JetBrains\ReSharper\v4.0\vs9.0\Plugins"
if not exist "%APPDATA%\JetBrains\ReSharper\v4.0\vs9.0\Plugins\NHibernate" mkdir "%APPDATA%\JetBrains\ReSharper\v4.0\vs9.0\Plugins\NHibernate"

if exist NHibernatePlugin.dll goto currentPath

copy NHibernatePlugin\bin\debug\NHibernatePlugin.dll "%APPDATA%\JetBrains\ReSharper\v4.0\vs9.0\Plugins\NHibernate"
copy NHibernatePlugin\bin\debug\NHibernatePlugin.pdb "%APPDATA%\JetBrains\ReSharper\v4.0\vs9.0\Plugins\NHibernate"
goto exit

:currentPath
copy NHibernatePlugin.dll "%APPDATA%\JetBrains\ReSharper\v4.0\vs9.0\Plugins\NHibernate"
copy NHibernatePlugin.pdb "%APPDATA%\JetBrains\ReSharper\v4.0\vs9.0\Plugins\NHibernate"


:exit
