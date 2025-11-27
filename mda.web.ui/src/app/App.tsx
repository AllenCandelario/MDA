import AppProviders from './provider'
import { AppRouter } from './router'

export default function App() {

  return (
    <AppProviders>
      <AppRouter />
    </AppProviders>
  )
}