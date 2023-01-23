import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import { createAuth0, authGuard } from "@auth0/auth0-vue";
import MainPage from "../views/MainPage.vue";
import SignupPage from "../views/SignPage.vue";
import TestPage from "../views/Test.vue";
import Profile from "../views/Profil.vue";
import Lesson from "@/views/Lesson.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "home",
    component: MainPage,
  },
  {
    path: "/signup",
    name: "signupPage",
    component: SignupPage,
  },
  {
    path: "/test",
    name: "test",
    component: TestPage,
  },
  {
    path: "/profile",
    name: "profile",
    component: Profile,
  },
  {
    path: "/lesson/:lessonId",
    name: "lesson",
    component: Lesson,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
