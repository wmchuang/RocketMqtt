import axios from 'axios';
import type { RouteRecordNormalized } from 'vue-router';
import { UserState } from '@/store/modules/user/types';
import { BasePageQueryRequestModel, PageQueryResponseModel } from '@/types/common'

export interface LoginData {
  username: string;
  password: string;
}

export interface LoginRes {
  token: string;
}

export interface PageRequest extends BasePageQueryRequestModel {
  userName: string;
}

export interface PageResult {
  userName: string;
  remark: string;
}

export interface AddUserRequest {
  userName: string;
  password: string;
  remark: string;
}

export function getPage(data: BasePageQueryRequestModel) {
  return axios.post<PageQueryResponseModel<PageResult>>('/api/user/PageList', data);
}

export function login(data: LoginData) {
  return axios.post<LoginRes>('/api/user/login', data);
}

export function add(data: AddUserRequest) {
  return axios.post<LoginRes>('/api/user/add', data);
}


export function logout() {
  return axios.post<LoginRes>('/api/user/logout');
}

export function getUserInfo() {
  return axios.post<UserState>('/api/user/info');
}

export function getMenuList() {
  return axios.post<RouteRecordNormalized[]>('/api/user/menu');
}
