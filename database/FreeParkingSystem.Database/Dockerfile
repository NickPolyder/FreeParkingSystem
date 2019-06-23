FROM microsoft/mssql-server-windows-developer AS Base

ENV ACCEPT_EULA="Y" \
	sa_password="S1mple@password"

WORKDIR /

COPY /bin/Debug .

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

HEALTHCHECK CMD [ "sqlcmd", "-Q", "select 1" ]

CMD .\start -sa_password $env:sa_password -ACCEPT_EULA $env:ACCEPT_EULA -attach_dbs \"$env:attach_dbs\" -Verbose