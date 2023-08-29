<script setup lang="ts">
import mqttConnect from './components/mqttConnect.vue';
import { connect } from "mqtt"

import { ref } from 'vue'


const connected = ref(false)

const client = connect('wss://test.mosquitto.org:8081');



</script>
<template>
    <div class="container">
        <Breadcrumb :items="['menu.tool', 'menu.tool.websocket']" />
        <a-space direction="vertical" :size="16" fill>
            <a-card class="general-card" title="连接">
                <mqttConnect></mqttConnect>
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
