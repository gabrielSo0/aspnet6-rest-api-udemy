FROM mysql:5.7.22
EXPOSE 3306
COPY RestAPI/db/migrations/ /home/database 
COPY RestAPI/db/dataset/ /home/database 
COPY RestAPI/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh