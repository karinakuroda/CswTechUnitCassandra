# Csw Tech Unit Cassandra


## Using Cassandra + Docker

### Run Docker

```bash
docker run --name some-cassandra -p 9042:9042 -p 9160:9160 -d cassandra:latest 
```

### Run CQL SH
>  CQL — Cassandra Query Language

```bash
docker exec -it some-cassandra cqlsh
```

### Run cqlsh commands

```sql
CREATE KEYSPACE IF NOT EXISTS cswKeyspace WITH REPLICATION = {'class':'SimpleStrategy', 'replication_factor':1};

CREATE TABLE cswkeyspace.order_status ( id UUID PRIMARY KEY, status text );

INSERT INTO cswkeyspace.order_status (id, status) VALUES (now(), 'New');
INSERT INTO cswkeyspace.order_status (id, status) VALUES (now(), 'In Progress');
INSERT INTO cswkeyspace.order_status (id, status) VALUES (now(), 'Shipped');
INSERT INTO cswkeyspace.order_status (id, status) VALUES (now(), 'In Delivery');
INSERT INTO cswkeyspace.order_status (id, status) VALUES (now(), 'Delivered');

```


### Others

```bash
docker inspect some-cassandra
docker exec -it some-cassandra nodetool status
```

```sql
USE cswKeyspace;
DROP TABLE cswkeyspace.order_status;
```

```bash
docker exec -it some-cassandra /bin/bash
```

> The Cassandra configuration files can be found in the conf directory of tarballs. For packages, the configuration files will be located in /etc/cassandra.

### Dependencies
* DataStax C# Driver for Apache Cassandra
* .NET Framework 4.6.1

***
## About Cassandra

###  What is Apache Cassandra?
> Apache Cassandra™, a top level Apache project born at Facebook and built on Amazon’s Dynamo and Google’s BigTable, is a distributed database for managing large amounts of structured data across many commodity servers, while providing highly available service and no single point of failure. 

### How to set up entity relations?

1. **Embedding** the referred document

2. Just putting the key of the referred document and storing the referred document independently. If the cardinality is something like 1 to 10000 ( **one to zillion**) reference storing should be done vice versa.

3. Using an intermediate document for relations if the cardinality is many to many.

![](./img/nosql_document_relationship.png "NoSQL Document Relationship")

### Keyspace
Where data is organized

Has 1 or more tables

Partition = where the data is located in the cluster

### Cassandra with 3 clusters
```bash
docker run --name cassandraCsw -d cassandra:2.2.8
docker run --name cassandraCsw2 -d --link cassandraCsw:cassandra cassandra:2.2.8
docker run --name cassandraCsw3 -d --link cassandraCsw2:cassandra cassandra:2.2.8
docker exec -it cassandraCsw nodetool status
```

### Replication Factor
>In simplest terms, the replication factor refers to the number of nodes that will act
as copies (replicas) of each row of data. If your replication factor is 3, then three
nodes in the ring will have copies of each row, and this replication is transparent
to clients.

>The replication factor essentially allows you to decide how much you want to pay
in performance to gain more consistency.

When replication factor = 1 and you have 3 node cluster:
Only 1 copy of the data being store to the keyspace, each node owns aprox. 33% of the data for the keyspace

![](./img/owns_replicationfactor1.png "Replication")

When replication factor = 3 and you have 3 node cluster
Each node owns 100% of the data for the keyspace

![](./img/owns_replicationfactor3.png "Replication")


### Links
* https://bitbucket.critical.pt/projects/ALCHEMISTSTRAINING/repos/techunits/browse/techunits/cassandra.md
* https://medium.com/@zwinny/cassandra-with-docker-915153e0592c
* https://github.com/datastax/csharp-driver
* https://medium.com/@talhaocakci/redesigning-persistence-entities-for-nosql-ebfd74a23de5
* https://github.com/datastax/csharp-driver