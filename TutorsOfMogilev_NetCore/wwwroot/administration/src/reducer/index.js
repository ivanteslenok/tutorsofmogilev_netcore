import { combineReducers } from 'redux';
import errorDialog from './errorDialog';
import tutors from './tutors';
import subjects from './subjects';
import contactTypes from './contactTypes';
import specializations from './specializations';
import cities from './cities';

export default combineReducers({
  errorDialog,
  tutors,
  subjects,
  contactTypes,
  specializations,
  cities
});
