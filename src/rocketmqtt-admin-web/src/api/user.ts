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
  userName: string;
}

export interface PageRequest extends BasePageQueryRequestModel {
  userName: string;
}

export interface PageResult {
  userId: string;
  userName: string;
  remark: string;
}

export interface AddUserRequest {
  userName: string;
  password: string;
  remark: string;
}

export interface UpdateUserRemarkRequest {
  userId: string;
  remark: string;
}

export interface DeleteUserRequest {
  userId: string;
}



export interface UpdateUserRequest extends UpdateUserRemarkRequest{
  newPassword: string;
  confirmPassword: string;
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


export function updateRemark(data: UpdateUserRemarkRequest) {
  return axios.post<LoginRes>('/api/user/updateRemark', data);
}


export function update(data: UpdateUserRequest) {
  return axios.post<LoginRes>('/api/user/update', data);
}

export function deleteUserApi(data: DeleteUserRequest) {
  return axios.post<boolean>('/api/user/delete', data);
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
