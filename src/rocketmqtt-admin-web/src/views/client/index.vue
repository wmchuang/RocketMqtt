<template>
  <div class="container">
    <Breadcrumb :items="['menu.client']" />
    <a-card class="general-card" :title="$t('menu.client.search')">
      <a-row>
        <a-col :flex="1">
          <a-form
            :model="formModel"
            :label-col-props="{ span: 6 }"
            :wrapper-col-props="{ span: 18 }"
            label-align="left"
          >
            <a-row :gutter="16">
              <a-col :span="8">
                <a-form-item field="clientId" :label="$t('client.form.clientId')">
                  <a-input
                    v-model="formModel.clientId"
                    :placeholder="$t('client.form.clientId')"
                  />
                </a-form-item>
              </a-col>
              
              <a-col :span="8">
                <a-form-item field="userName" :label="$t('client.form.userName')">
                  <a-input
                    v-model="formModel.name"
                    :placeholder="$t('client.form.userName')"
                  />
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
              {{ $t('client.form.search') }}
            </a-button>
            <a-button @click="reset">
              <template #icon>
                <icon-refresh />
              </template>
              {{ $t('client.form.reset') }}
            </a-button>
          </a-space>
        </a-col>
      </a-row>
      <a-divider style="margin-top: 0" />
      <a-table
        row-key="id"
        :loading="loading"
        :pagination="pagination"
        :columns="(cloneColumns as TableColumnData[])"
        :data="renderData"
        :bordered="false"
        :size="size"
        @page-change="onPageChange"
      >
        <template #index="{ rowIndex }">
          {{ rowIndex + 1 + (pagination.pageIndex - 1) * pagination.pageSize }}
        </template>
        <template #operations="{ record }">
          <a-button v-permission="['admin']" type="text" size="small"  @click="Disconnect(record.clientId)" >
            {{ $t('client.columns.operations.disconnect') }}
          </a-button>
        </template>
      </a-table>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
  import {  computed, ref, reactive, watch } from 'vue';
  import { useI18n } from 'vue-i18n';
  import useLoading from '@/hooks/loading';
  import type { TableColumnData } from '@arco-design/web-vue/es/table/interface';
  import { PageRequest,PageResult,getPage,disconnect } from '@/api/conninfo'
  import { Pagination } from '@/types/global';
  import cloneDeep from 'lodash/cloneDeep';
  import { Message } from '@arco-design/web-vue';

  type Column = TableColumnData & { checked?: true };
  const generateFormModel = () => {
    return {
      clientId: '',
      userName: ''
    };
  };
  const { loading, setLoading } = useLoading(true);
  const { t } = useI18n();
  const renderData = ref<PageResult[]>([]);
  const formModel = ref(generateFormModel());
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
      title: t('client.columns.index'),
      dataIndex: 'index',
      slotName: 'index',
    },
    {
      title: t('client.columns.clientId'),
      dataIndex: 'clientId',
    },
    {
      title: t('client.columns.userName'),
      dataIndex: 'userName',
    },
    {
      title: t('client.columns.endpoint'),
      dataIndex: 'endpoint',
    },
    {
      title: t('client.columns.keepAlive'),
      dataIndex: 'keepAlive',
    },
    {
      title: t('client.columns.createTime'),
      dataIndex: 'createTime',
    },
    {
      title: t('client.columns.operations'),
      dataIndex: 'operations',
      slotName: 'operations',
    },
  ]);
  const fetchData = async (
    params: PageRequest = {
      pageIndex: 1,
      pageSize: 15,
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
    fetchData({ ...basePagination, pageIndex });
  };

  fetchData();
  const reset = () => {
    formModel.value = generateFormModel();
  };

  const Disconnect = async (id: string) => {
    const { data } = await disconnect(id);
    console.log(data);
    if(data === true)
    {
      //  fetchData();  //删除这一行
    }
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
    name: 'SearchConsul',
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
