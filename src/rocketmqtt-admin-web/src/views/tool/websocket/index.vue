<script setup lang="ts">
import connect from './components/connect.vue';
</script>
<template>
    <div class="container">
        <Breadcrumb :items="['menu.tool', 'menu.tool.websocket']" />
        <a-space direction="vertical" :size="16" fill>
            <a-card class="general-card" title="连接">
                <connect></connect>
            </a-card>
            <a-card class="general-card" title="订阅">
                <!-- <connect></connect> -->
            </a-card>
            <a-card class="general-card" title="消息">
                <!-- <connect></connect> -->
            </a-card>
            <OperationLog />
        </a-space>
    </div>
</template>


<style scoped lang="less">
.container {
    padding: 0 20px 20px 20px;
}
</style>
