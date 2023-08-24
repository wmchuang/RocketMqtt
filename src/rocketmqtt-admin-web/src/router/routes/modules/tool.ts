import { DEFAULT_LAYOUT } from '../base';
import { AppRouteRecordRaw } from '../types';

const COMMON: AppRouteRecordRaw = {
  path: '/tool',
  name: 'tool',
  component: DEFAULT_LAYOUT,
  meta: {
    locale: 'menu.tool',
    icon: 'icon-tool',
    requiresAuth: true,
    order: 5,
  },
  children: [
    {
      path: 'websocket',
      name: 'websocket',
      component: () => import('@/views/tool/websocket/index.vue'),
      meta: {
        locale: 'menu.tool.websocket',
        requiresAuth: true,
        roles: ['*'],
      },
    }
  ],
};

export default COMMON;
