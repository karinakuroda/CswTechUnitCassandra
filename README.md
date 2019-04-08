# CswTechUnitCassandra

# Cassandra

docker run --name some-cassandra -p 9042:9042 -p 9160:9160 -d cassandra:latest 

docker exec -it some-cassandra cqlsh
CREATE KEYSPACE IF NOT EXISTS cswKeyspace WITH REPLICATION = {'class':'SimpleStrategy', 'replication_factor':1};

CREATE TABLE cswkeyspace.order_status ( id UUID PRIMARY KEY, status text );
INSERT INTO cswkeyspace.order_status (id, status) VALUES (now(), 'New');
INSERT INTO cswkeyspace.order_status (id, status) VALUES (now(), 'In Progress');
INSERT INTO cswkeyspace.order_status (id, status) VALUES (now(), 'Shipped');
INSERT INTO cswkeyspace.order_status (id, status) VALUES (now(), 'In Delivery');
INSERT INTO cswkeyspace.order_status (id, status) VALUES (now(), 'Delivered');


# Outros comandos:
docker inspect some-cassandra
docker exec -it some-cassandra nodetool status
docker exec -it some-cassandra /bin/bash

DROP TABLE cswkeyspace.order_status;
USE cswKeyspace;

The Cassandra configuration files can be found in the conf directory of tarballs. For packages, the configuration files will be located in /etc/cassandra.

# Links Úteis
https://bitbucket.critical.pt/projects/ALCHEMISTSTRAINING/repos/techunits/browse/techunits/cassandra.md
https://medium.com/@zwinny/cassandra-with-docker-915153e0592c
https://github.com/datastax/csharp-driver
