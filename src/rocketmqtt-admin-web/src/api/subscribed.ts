import { BasePageQueryRequestModel, PageQueryResponseModel } from '@/types/common'
import axios from 'axios'

export interface PageRequest extends BasePageQueryRequestModel {
    topicName: string;
    clientId: string;
}

export interface PageResult {
    topicName: string;
    clientId: string;
    qos: number;
}

export function getPage(data: BasePageQueryRequestModel) {
    return axios.post<PageQueryResponseModel<PageResult>>('/api/Subscribed/PageList', data);
}