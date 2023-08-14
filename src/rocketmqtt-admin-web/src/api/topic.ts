import { BasePageQueryRequestModel, PageQueryResponseModel } from '@/types/common'
import axios from 'axios'

export interface PageRequest extends BasePageQueryRequestModel {
    topicName: string;
}

export interface PageResult {
    topicName: string;
    node: string;
}

export function getPage(data: BasePageQueryRequestModel) {
    return axios.post<PageQueryResponseModel<PageResult>>('/api/Topic/PageList', data);
}