FROM microsoft/aspnet
COPY . /app
WORKDIR /app
RUN ["kpm", "restore"]
EXPOSE 5005
ENTRYPOINT ["k", "kestrel"]
