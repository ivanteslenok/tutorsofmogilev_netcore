import React from 'react';
import { Provider } from 'react-redux';
import store from './store';
import Menu from './components/Menu';
import TutorsPage from './components/routes/TutorsPage';
import SubjectsPage from './components/routes/SubjectsPage';
import ContactTypesPage from './components/routes/ContactTypesPage';
import SpecializationsPage from './components/routes/SpecializationsPage';
import CitiesPage from './components/routes/CitiesPage';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import ErrorDialogContainer from './containers/ErrorDialogContainer';

export default function App() {
  return (
    <Provider store={store}>
      <>
        <Router>
          <>
            <Menu />
            <Switch>
              <Route path="/tutors" component={TutorsPage} />
              <Route path="/subjects" component={SubjectsPage} />
              <Route path="/contact-types" component={ContactTypesPage} />
              <Route path="/specializations" component={SpecializationsPage} />
              <Route path="/cities" component={CitiesPage} />
              <Route path="*" component={TutorsPage} />
            </Switch>
          </>
        </Router>
        <ErrorDialogContainer />
      </>
    </Provider>
  );
}
