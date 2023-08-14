import { DEFAULT_LAYOUT } from '../base';
import { AppRouteRecordRaw } from '../types';

const TOPIC: AppRouteRecordRaw = {
    path: '/topic',
    name: 'topic',
    component: DEFAULT_LAYOUT,
    redirect: '/topic/index',
    meta: {
        locale: 'menu.topic',
        requiresAuth: true,
        icon: 'icon-branch',
        order: 2,
        hideInMenu: false,
    },
    children: [
        {
          path: '/topic/index',
          name: 'topicIndex',
          component: () => import('@/views/topic/index.vue'),
          meta: {
            locale: 'menu.topic',
            requiresAuth: true,
            roles: ['*'],
            hideInMenu: true,
            activeMenu: "topic"
          },
        },
      ],
};

export default TOPIC;
