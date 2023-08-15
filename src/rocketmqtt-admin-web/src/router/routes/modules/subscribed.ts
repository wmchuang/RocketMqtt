import { DEFAULT_LAYOUT } from '../base';
import { AppRouteRecordRaw } from '../types';

const SUBSCRIBED: AppRouteRecordRaw = {
    path: '/subscribed',
    name: 'subscribed',
    component: DEFAULT_LAYOUT,
    redirect: '/subscribed/index',
    meta: {
        locale: 'menu.subscribed',
        requiresAuth: true,
        icon: 'icon-ear',
        order: 3,
        hideInMenu: false,
    },
    children: [
        {
          path: '/subscribed/index',
          name: 'subscribedIndex',
          component: () => import('@/views/subscribed/index.vue'),
          meta: {
            locale: 'menu.subscribed',
            requiresAuth: true,
            roles: ['*'],
            hideInMenu: true,
            activeMenu: "subscribed"
          },
        },
      ],
};

export default SUBSCRIBED;
