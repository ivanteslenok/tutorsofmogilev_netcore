import { combineReducers } from 'redux';
import dialogs from './dialogs';
import tutors from './tutors';
import subjects from './subjects';
import contactTypes from './contactTypes';
import specializations from './specializations';
import districts from './districts';

export default combineReducers({
  dialogs,
  tutors,
  subjects,
  contactTypes,
  specializations,
  districts
});
