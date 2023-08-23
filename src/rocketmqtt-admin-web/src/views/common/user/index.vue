<template>
  <div class="container">
    <Breadcrumb :items="['menu.common', 'menu.common.user']" />
    <a-card class="general-card" :title="$t('menu.common.user')">
      <a-row>
        <a-col :flex="1">
          <a-form :model="formModel" :label-col-props="{ span: 6 }" :wrapper-col-props="{ span: 18 }" label-align="left">
            <a-row :gutter="16">
              <a-col :span="8">
                <a-form-item field="userName" :label="$t('user.form.userName')">
                  <a-input v-model="formModel.userName" :placeholder="$t('user.form.userName')" />
                </a-form-item>
              </a-col>


            </a-row>
          </a-form>
        </a-col>
        <a-divider style="height: 32px" direction="vertical" />
        <a-col :flex="'86px'" style="text-align: right">
          <a-space direction="horizontal" :size="18">
            <a-button type="primary" @click="search">
              <template #icon>
                <icon-search />
              </template>
              {{ $t('user.form.search') }}
            </a-button>
            <a-button @click="reset">
              <template #icon>
                <icon-refresh />
              </template>
              {{ $t('user.form.reset') }}
            </a-button>
          </a-space>
        </a-col>
      </a-row>
      <a-divider style="margin-top: 0" />

      <a-row style="margin-bottom: 16px">
        <a-col :span="12">
          <a-space>
            <a-button type="primary" @click="create(true)">
              <template #icon>
                <icon-plus />
              </template>
              {{ $t('user.operation.create') }}
            </a-button>
          </a-space>
        </a-col>

      </a-row>
      <a-table row-key="id" :loading="loading" :pagination="pagination" :columns="(cloneColumns as TableColumnData[])"
        :data="renderData" :bordered="false" @page-change="onPageChange">
        <template #index="{ rowIndex }">
          {{ rowIndex + 1 + (pagination.pageIndex - 1) * pagination.pageSize }}
        </template>
        <template #operations="{ record }">
          <a-button type="text" size="small" @click="editHandle(record)">
            {{ $t('user.columns.operations.edit') }}
          </a-button>
        </template>
      </a-table>
    </a-card>

    <a-modal v-model:visible="visible" :title="$t('user.addForm')" :mask-closable="false" @ok="handleOk">
      <a-form :model="addForm">
        <a-form-item field="userName" :label="$t('user.addForm.userName')">
          <a-input v-model="addForm.userName" />
        </a-form-item>
        <a-form-item field="password" :label="$t('user.addForm.password')">
          <a-input v-model="addForm.password" />
        </a-form-item>
        <a-form-item field="remark" :label="$t('user.addForm.remark')">
          <a-input v-model="addForm.remark" />
        </a-form-item>
      </a-form>
    </a-modal>

    <edit v-model:visible="editvisible" @closeEditModal="closeEditModal" :user="currenUser"></edit>
  </div>
</template>

<script lang="ts" setup>
import { computed, ref, reactive, watch } from 'vue';
import { useI18n } from 'vue-i18n';
import useLoading from '@/hooks/loading';
import type { TableColumnData } from '@arco-design/web-vue/es/table/interface';
import { PageRequest, PageResult, getPage, AddUserRequest, add } from '@/api/user'
import { Pagination } from '@/types/global';
import cloneDeep from 'lodash/cloneDeep';
import { Message } from '@arco-design/web-vue';
import edit from './edit.vue'


type Column = TableColumnData & { checked?: true };
const generateFormModel = () => {
  return {
    userName: '',
  };
};
const { loading, setLoading } = useLoading(true);
const { t } = useI18n();
const renderData = ref<PageResult[]>([]);
const formModel = ref(generateFormModel());
const addForm = ref<AddUserRequest>({
  userName: '',
  password: '',
  remark: '',
});

const currenUser = reactive<PageResult>({
  userId: '',
  userName: '',
  remark: '',
});
const visible = ref(false);
const editvisible = ref(false);
const basePagination: Pagination = {
  pageIndex: 1,
  pageSize: 10,
};
const pagination = reactive({
  ...basePagination,
});
const cloneColumns = ref<Column[]>([]);
const showColumns = ref<Column[]>([]);
const columns = computed<TableColumnData[]>(() => [
  {
    title: t('user.columns.index'),
    dataIndex: 'index',
    slotName: 'index',
  },
  {
    title: t('user.columns.userName'),
    dataIndex: 'userName',
  },
  {
    title: t('user.columns.remark'),
    dataIndex: 'remark',
  },
  {
    title: t('user.columns.operations'),
    dataIndex: 'operations',
    slotName: 'operations',
  },
]);
const fetchData = async (
  params: PageRequest = {
    pageIndex: 1,
    pageSize: 15,
    userName: "",
  }
) => {
  setLoading(true);
  try {
    const { data } = await getPage(params);
    console.log(data);
    renderData.value = data.items;
    pagination.pageIndex = params.pageIndex;
    pagination.total = data.totalCount;
  } catch (err) {
    // you can report use errorHandler or other
  } finally {
    setLoading(false);
  }
};

const search = () => {
  fetchData({
    ...basePagination,
    ...formModel.value,
  } as unknown as PageRequest);
};

const onPageChange = (pageIndex: number) => {
  basePagination.pageIndex = pageIndex;
  fetchData({
    ...basePagination,
    ...formModel.value,
  } as unknown as PageRequest);
};

const create = (create: boolean) => {
  visible.value = true;
}

const editHandle = (record: PageResult) => {
  currenUser.userId = record.userId;
  currenUser.userName = record.userName;
  currenUser.remark = record.remark;
  editvisible.value = true;
}

const handleOk = async () => {
  visible.value = false;
  await add(addForm.value);

  addForm.value = {
    userName: '',
    password: '',
    remark: '',
  };
  Message.success({
    content: '操作成功！',
    duration: 5 * 1000,
  });
  fetchData();
};

const closeEditModal = () => {
  editvisible.value = false;
  fetchData();
};

fetchData();
const reset = () => {
  formModel.value = generateFormModel();
};


watch(
  () => columns.value,
  (val) => {
    cloneColumns.value = cloneDeep(val);
    cloneColumns.value.forEach((item, index) => {
      item.checked = true;
    });
    showColumns.value = cloneDeep(cloneColumns.value);
  },
  { deep: true, immediate: true }
);
</script>

<script lang="ts">
export default {
  name: 'User',
};
</script>

<style scoped lang="less">
.container {
  padding: 0 20px 20px 20px;
}

:deep(.arco-table-th) {
  &:last-child {
    .arco-table-th-item-title {
      margin-left: 16px;
    }
  }
}
</style>
