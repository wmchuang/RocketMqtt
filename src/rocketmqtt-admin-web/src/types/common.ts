export interface PageQueryResponseModel<T> {
    pageIndex: number;
    pageSize: number;
    totalCount: number;
    totalPages: number;
    items: T[];
    hasPrevPages: boolean;
    hasNextPages: boolean;
  }
  
  export interface BasePageQueryRequestModel {
    pageIndex: number;
    pageSize: number;
  }