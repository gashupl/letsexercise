import { ExerciseRecord } from './events/ExerciseRecord'
import { App} from './App'

export function initialize() {
  console.log('Initialize main ODX library')
}

exports.ExerciseRecord = ExerciseRecord
exports.App = App
