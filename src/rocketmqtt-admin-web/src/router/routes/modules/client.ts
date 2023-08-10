import { DEFAULT_LAYOUT } from '../base';
import { AppRouteRecordRaw } from '../types';

const CLIENT: AppRouteRecordRaw = {
    path: '/client',
    name: 'client',
    component: DEFAULT_LAYOUT,
    redirect: '/client/index',
    meta: {
        locale: 'menu.client',
        requiresAuth: true,
        icon: 'icon-branch',
        order: -1,
        hideInMenu: false,
    },
    children: [
        {
          path: '/client/index',
          name: 'clientIndex',
          component: () => import('@/views/client/index.vue'),
          meta: {
            locale: 'menu.client',
            requiresAuth: true,
            roles: ['*'],
            hideInMenu: true,
            activeMenu: "client"
          },
        },
      ],
};

export default CLIENT;
