import { UserRoleEnum } from "@infrastructure/apis/client/models";
import { useOwnUserHasRole } from "@infrastructure/hooks/useOwnUser";
import { AppIntlProvider } from "@presentation/components/ui/AppIntlProvider";
import { ToastNotifier } from "@presentation/components/ui/ToastNotifier";
import { HomePage } from "@presentation/pages/HomePage";
import { LoginPage } from "@presentation/pages/LoginPage";
import { RegisterPage } from "@presentation/pages/RegisterPage";
import { UserFilesPage } from "@presentation/pages/UserFilesPage";
import { UsersPage } from "@presentation/pages/UsersPage";
import { BookPage } from "@presentation/pages/BookPage";
import { AuthorPage } from "@presentation/pages/AuthorPage";
import { FeedbackPage } from "@presentation/pages/FeedbackPage";
import { Route, Routes } from "react-router-dom";
import { AppRoute } from "routes";

export function App() {
  const isAdmin = useOwnUserHasRole(UserRoleEnum.Admin);
  const isClient = useOwnUserHasRole(UserRoleEnum.Client);

  return <AppIntlProvider> {/* AppIntlProvider provides the functions to search the text after the provides string ids. */}
      <ToastNotifier />
      {/* This adds the routes and route mappings on the various components. */}
      <Routes>
        <Route path={AppRoute.Index} element={<HomePage />} /> {/* Add a new route with a element as the page. */}
        <Route path={AppRoute.Login} element={<LoginPage />} />
        <Route path={AppRoute.Register} element={<RegisterPage />} />
        {isAdmin && <Route path={AppRoute.Users} element={<UsersPage />} />} {/* If the user doesn't have the right role this route shouldn't be used. */}
        {isAdmin && <Route path={AppRoute.UserFiles} element={<UserFilesPage />} />}
        {isAdmin && <Route path={AppRoute.Book} element={<BookPage />} />}
        {isAdmin && <Route path={AppRoute.Author} element={<AuthorPage />} />}
        {isAdmin && <Route path={AppRoute.Feedback} element={<FeedbackPage />} />}
      </Routes>
    </AppIntlProvider>
}
