<script lang="ts" setup>
import { useI18n } from 'vue-i18n';
import { PageResult, UpdateUserRequest,UpdateUserRemarkRequest,updateRemark,update } from '@/api/user'
import {  ref,reactive,  watch,watchEffect } from 'vue';

const emits = defineEmits(['update:visible', 'closeEditModal'])

const showUpdatePasword = ref(false)
const validateTrigger = ref<('change' | 'input' | 'focus' | 'blur')[]>(['change', 'input'])
const formModel = reactive({
    userId: '',
    userName: '',
    remark: '',
    newPassword: '',
    confirmPassword: '',
});

const props = defineProps<{
  visible?: boolean,
  user?: PageResult
}>()

function assign() {
  let uobj = props.user;
  if(uobj != null)
  {
    formModel.userId = uobj.userId;
    formModel.userName = uobj.userName;
    formModel.remark = uobj.remark;
  }
}

const handleOk = async () =>  {
  if(showUpdatePasword.value == false)
  {
    let request: UpdateUserRemarkRequest = {
      userId: formModel.userId,
      remark: formModel.remark,
    };
    await updateRemark(request);
  }else{
    let request: UpdateUserRequest = {
      userId: formModel.userId,
      remark: formModel.remark,
      newPassword: formModel.newPassword,
      confirmPassword: formModel.confirmPassword,
    };
    await update(request);
  }
  emits('closeEditModal')
}

const handleCancel = () => {
    emits('closeEditModal')
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
         
          <a-form-item  field="newPasword" label="新密码" >
              <a-input
               v-model="formModel.newPassword"
                allow-clear />
          </a-form-item>
        
          <a-form-item  field="confirmNewPasword" label="确认新密码" >
              <a-input
              v-model="formModel.confirmPassword"
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