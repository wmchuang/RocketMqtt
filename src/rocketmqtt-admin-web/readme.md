### 开发记录

- 新建菜单步骤

    1、在api下建立对应的接口文件

    2、在routes\modules 下建立对应路由文件，文件中配置好icon,path，有需要的话还可以配置二级路由。 上级index.ts中会加载modules中的所有ts文件

    3、views文件夹下建立相应的页面文件夹，里面 包含 index.vue、mock.ts、locale文件夹

    4、locale文件夹下中的zh-CN.ts和en-US.ts中 引入views下locale文件夹中的文件、并配置刚才添加的路由名称
