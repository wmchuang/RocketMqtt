<script lang="ts" setup>
import { useI18n } from 'vue-i18n';
import { UpdateUserRequest } from '@/api/user'
import {  ref,  watch,watchEffect } from 'vue';

const emits = defineEmits(['update:visible', 'save'])

const showUpdatePasword = ref(false)
const validateTrigger = ref<('change' | 'input' | 'focus' | 'blur')[]>(['change', 'input'])
const formModel = ref<UpdateUserRequest>({
    userName: '',
    password: '',
    remark: '',
  });

const props = defineProps<{
  visible?: boolean,
  user?: UpdateUserRequest
}>()

function assign() {
  let uobj = props.user;
  if(uobj != null)
  {
    formModel.value.userName = uobj.userName;
    formModel.value.remark = uobj.remark;
  }
}

const handleOk = () => {
    emits('save')
}

const handleCancel = () => {
    emits('save')
}

const updateshowUpdate = () => {
  showUpdatePasword.value = !showUpdatePasword.value;
}


watch(() => props.visible,(val) => {
  if (val) assign()
})
</script>

<template>
    <a-modal 
        :visible="visible" 
        :title="$t('user.addForm')" 
        :mask-closable="false"
        @cancel="handleCancel"
        @ok="handleOk">

        <a-form-item  field="userName" label="用户名" >
            <a-input
              v-model="formModel.userName"
              :disabled="true"
              allow-clear />
        </a-form-item>

        <template v-if="showUpdatePasword">
          <a-form-item  field="pasword" label="旧密码" >
              <a-input
                allow-clear />
          </a-form-item>

          <a-form-item  field="newPasword" label="新密码" >
              <a-input
                allow-clear />
          </a-form-item>
        
          <a-form-item  field="confirmNewPasword" label="确认新密码" >
              <a-input
                allow-clear />
          </a-form-item>
      </template>
        <a-form-item  field="remark" label="备注">
          <a-input
              v-model="formModel.remark"
              allow-clear />
        </a-form-item>
      
        <a-button type="text" @click="updateshowUpdate">修改密码</a-button>
    </a-modal>
</template>





<style scoped lang="less"></style>