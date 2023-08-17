import { DEFAULT_LAYOUT } from '../base';
import { AppRouteRecordRaw } from '../types';

const COMMON: AppRouteRecordRaw = {
  path: '/common',
  name: 'common',
  component: DEFAULT_LAYOUT,
  meta: {
    locale: 'menu.common',
    icon: 'icon-common',
    requiresAuth: true,
    order: 7,
  },
  children: [
    {
      path: 'user',
      name: 'user',
      component: () => import('@/views/common/user/index.vue'),
      meta: {
        locale: 'menu.common.user',
        requiresAuth: true,
        roles: ['*'],
      },
    }
  ],
};

export default COMMON;
