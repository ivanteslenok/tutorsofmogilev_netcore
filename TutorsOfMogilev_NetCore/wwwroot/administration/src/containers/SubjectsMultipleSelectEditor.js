import { connect } from 'react-redux';
import { getSortedSubjects } from './../selectors/subjects';
import * as tutorAC from '../AC/tutor';
import subjectAC from '../AC/subject';
import MultipleSelectEditor from '../components/MultipleSelectEditor';

const mapStateToProps = (state, ownProps) => {
  return {
    label: 'Предметы',
    availableValues: getSortedSubjects(state),
    currentValues: ownProps.currentValues,
    editId: ownProps.editId,
    loading: state.subjects.loading,
    loaded: state.subjects.loaded
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(subjectAC.loadEntities());
    },
    update: (id, data) => {
      dispatch(tutorAC.updateTutorSubjects(id, data));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(MultipleSelectEditor);
