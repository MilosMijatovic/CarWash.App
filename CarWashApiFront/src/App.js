import React, { useContext, useState } from "react";
import { ThemeProvider } from "@material-ui/styles";
import { BrowserRouter, Redirect, Route, Switch } from "react-router-dom";
import Theme from "./component/ui/Theme";
import Footer from "./component/ui/Footer";
import HomePage from "./pages/HomePage";
import LogInPage from "./pages/LogInPage";
import ProfilePage from "./pages/ProfilePage";

import Header from "./component/ui/Header";
import AuthContext from "./context/Auth-context";

function App() {
  const authCtx = useContext(AuthContext);
  const { isLogin } = authCtx;
  const [selectedIndex, setSelectedIndex] = useState(0);
  const [value, setValue] = useState(0);

  return (
    <ThemeProvider theme={Theme}>
      <BrowserRouter>
        <Header
          value={value}
          setValue={setValue}
          selectedIndex={selectedIndex}
          setSelectedIndex={setSelectedIndex}
        />
        <Switch>
          <Route path="/" exact>
            {isLogin ? <HomePage /> : <Redirect to="/login" />}
          </Route>
          <Route path="/login">
            {!isLogin ? <LogInPage /> : <Redirect to="/" />}
          </Route>
          <Route path="/profile">
          {isLogin ? <ProfilePage /> : <Redirect to="/login" />}
          </Route>
        </Switch>
        {window.location.pathname !== "/login" && (
          <Footer
            value={value}
            setValue={setValue}
            selectedIndex={selectedIndex}
            setSelectedIndex={setSelectedIndex}
          />
        )}
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;
