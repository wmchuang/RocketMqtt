# RocketMqtt



### 开发记录

- 实体： 客户端、主题、订阅
- 客户端订阅主题时，首先会插入一条订阅的记录，然后发布一条领域事件信息，在领域事件信息处理方法中，插入订阅的主题信息
-