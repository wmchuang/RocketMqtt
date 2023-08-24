<script setup lang="ts">
import { computed, reactive } from 'vue';

const formModel = reactive({
    host: 'localhost',
    port: '8080',
    path: '/mqtt',
});

//计算属性
let url = computed(() => {
    return "wss://" + formModel.host + ':' + formModel.port + formModel.path;
})
</script>
<template>
    <a-form layout="vertical" :model="formModel">
        <a-space direction="vertical" :size="16">
            <a-card class="general-card">

                <a-row :gutter="80">
                    <a-col :span="8">
                        <a-form-item label="主机地址" field="video.mode">
                            <a-input v-model="formModel.host"></a-input>
                        </a-form-item>
                    </a-col>
                    <a-col :span="8">
                        <a-form-item label="端口" field="video.mode">
                            <a-input v-model="formModel.port"></a-input>
                        </a-form-item>
                    </a-col>
                    <a-col :span="8">
                        <a-form-item label="Path" field="video.mode">
                            <a-input v-model="formModel.path"></a-input>
                        </a-form-item>
                    </a-col>
                </a-row>
                <a-row :gutter="80">
                    <a-col :span="8">
                        <a-form-item label="客户端 ID" field="video.mode">
                            <a-input></a-input>
                        </a-form-item>
                    </a-col>
                    <a-col :span="8">
                        <a-form-item label="用户名" field="video.mode">
                            <a-input></a-input>
                        </a-form-item>
                    </a-col>
                    <a-col :span="8">
                        <a-form-item label="密码" field="video.mode">
                            <a-input></a-input>
                        </a-form-item>
                    </a-col>
                </a-row>
                <a-row :gutter="80">
                    <a-col :span="8">
                        <a-form-item label="心跳（秒）" field="video.mode">
                            <a-input></a-input>
                        </a-form-item>
                    </a-col>
                </a-row>
            </a-card>
        </a-space>

        <a-row>
            <a-col :span="2">
                <a-checkbox :model-value="true">清除会话</a-checkbox>
            </a-col>
            <a-col :span="3">
                <a-checkbox value="1">SSL</a-checkbox>
            </a-col>
            <a-col :span="3" class="success">
                {{ url }}
            </a-col>
        </a-row>

        <a-row style="margin-top:30px">
            <div class="actions">
                <a-space :size="large">
                    <a-button type="primary" status="success">连接</a-button>
                    <a-button type="primary" status="danger" disabled>断开连接</a-button>

                    当前状态： <text class="error">未连接</text>
                </a-space>
            </div>
        </a-row>
    </a-form>
</template>

<style scoped lang="less">
    .success{
        color:rgb(66, 216, 133)
    }

    .error{
        color:rgb(255, 109, 109)
    }
    
</style>