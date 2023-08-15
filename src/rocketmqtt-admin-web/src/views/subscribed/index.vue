<template>
  <div class="container">
    <Breadcrumb :items="['menu.subscribed']" />
    <a-card class="general-card" :title="$t('menu.subscribed.search')">
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
                <a-form-item field="clientId" :label="$t('subscribed.form.clientId')">
                  <a-input
                    v-model="formModel.topicName"
                    :placeholder="$t('subscribed.form.clientId')"
                  />
                </a-form-item>
              </a-col>
              
              <a-col :span="8">
                <a-form-item field="topicName" :label="$t('subscribed.form.topicName')">
                  <a-input
                    v-model="formModel.topicName"
                    :placeholder="$t('subscribed.form.topicName')"
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
              {{ $t('subscribed.form.search') }}
            </a-button>
            <a-button @click="reset">
              <template #icon>
                <icon-refresh />
              </template>
              {{ $t('subscribed.form.reset') }}
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
        @page-change="onPageChange"
      >
        <template #index="{ rowIndex }">
          {{ rowIndex + 1 + (pagination.pageIndex - 1) * pagination.pageSize }}
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
  import { PageRequest,PageResult,getPage } from '@/api/subscribed'
  import { Pagination } from '@/types/global';
  import cloneDeep from 'lodash/cloneDeep';
  import { Message } from '@arco-design/web-vue';

  type Column = TableColumnData & { checked?: true };
  const generateFormModel = () => {
    return {
      topicName: '',
      clientId: '',
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
  const columns = computed<TableColumnData[]>(() => [
    {
      title: t('subscribed.columns.index'),
      dataIndex: 'index',
      slotName: 'index',
    },
    {
      title: t('subscribed.columns.clientId'),
      dataIndex: 'clientId',
    },
    {
      title: t('subscribed.columns.topicName'),
      dataIndex: 'topicName',
    },
    {
      title: t('subscribed.columns.qos'),
      dataIndex: 'qos',
    },
    
  ]);
  const fetchData = async (
    params: PageRequest = {
      pageIndex: 1,
      pageSize: 15,
      topicName: "",
      clientId: '',
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
    },
    { deep: true, immediate: true }
  );
</script>

<script lang="ts">
  export default {
    name: 'SearchSubscribed',
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
