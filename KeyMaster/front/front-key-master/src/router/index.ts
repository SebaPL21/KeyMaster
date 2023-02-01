import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Exercises from "../views/Exercises.vue";
import SignupPage from "../views/SignPage.vue";
import TestPage from "../views/Test.vue";
import Profile from "../views/Profil.vue";
import Lesson from "@/views/Lesson.vue";
import WelcomeSite from "@/views/WelcomeSite.vue";
import verify from "@/views/Verify.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/Exercises",
    name: "Exercises",
    component: Exercises,
  },
  {
    path: "/",
    name: "WelcomeSite",
    component: WelcomeSite,
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
    path: "/verify",
    name: "verify",
    component: verify,
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
