import { BasePageQueryRequestModel, PageQueryResponseModel } from '@/types/common'
import axios from 'axios'

export interface PageRequest extends BasePageQueryRequestModel {
    clientId: string;
    userName: string;
}

export interface PageResult {
    id: string;
    clientId: string;
    userName: string;
    endpoint: string;
    keepAlive: number;
    createTime: string;
}

export function getPage(data: BasePageQueryRequestModel) {
    return axios.post<PageQueryResponseModel<PageResult>>('/api/ConnInfo/PageList', data);
}

export function disconnect(id: string) {
    return axios.get<boolean>(`/api/ConnInfo/Disconnect/${id}`);
}
  