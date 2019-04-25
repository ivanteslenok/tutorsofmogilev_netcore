import { connect } from 'react-redux';
import * as tutorAC from '../AC/tutor';
import * as cityAC from '../AC/city';
import * as subjectAC from '../AC/subject';
import TutorsFilter from '../components/TutorsFilter';

const mapStateToProps = state => {
  return {
    isLookupValuesLoaded: state.cities.loaded && state.subjects.loaded,
    cities: state.cities.items,
    cityId: state.tutors.filter.cityId,
    subjects: state.subjects.items,
    subjectId: state.tutors.filter.subjectId
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadLookupValues: () => {
      dispatch(cityAC.loadCities());
      dispatch(subjectAC.loadSubjects());
    },
    setCityId: cityId => {
      dispatch(tutorAC.setFilter({ cityId }));
    },
    setSubjectId: subjectId => {
      dispatch(tutorAC.setFilter({ subjectId }));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(TutorsFilter);
