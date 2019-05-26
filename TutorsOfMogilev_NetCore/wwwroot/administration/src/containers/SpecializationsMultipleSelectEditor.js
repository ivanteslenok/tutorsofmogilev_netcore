import { connect } from 'react-redux';
import { getSortedSpecializations } from './../selectors/specializations';
import * as tutorAC from '../AC/tutor';
import specializationAC from '../AC/specialization';
import MultipleSelectEditor from '../components/MultipleSelectEditor';

const mapStateToProps = (state, ownProps) => {
  return {
    label: 'Специализации',
    availableValues: getSortedSpecializations(state),
    currentValues: ownProps.currentValues,
    editId: ownProps.editId,
    loading: state.specializations.loading,
    loaded: state.specializations.loaded
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadData: () => {
      dispatch(specializationAC.loadEntities());
    },
    update: (id, data) => {
      dispatch(tutorAC.updateTutorSpecializations(id, data));
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(MultipleSelectEditor);
