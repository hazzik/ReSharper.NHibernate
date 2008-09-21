if not exist "%APPDATA%\JetBrains\ReSharper\v4.1\vs9.0\Plugins\NHibernate" goto exit

del "%APPDATA%\JetBrains\ReSharper\v4.1\vs9.0\Plugins\NHibernate"\NHibernatePlugin.dll
del "%APPDATA%\JetBrains\ReSharper\v4.1\vs9.0\Plugins\NHibernate"\NHibernatePlugin.pdb


:exit
