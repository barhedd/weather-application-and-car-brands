import { render, screen } from "@testing-library/react"
import userEvent from "@testing-library/user-event"
import WeatherSearch from "@/features/weather/components/WeatherSearch"
import * as weatherService from "@/features/weather/services/fetchWeather"

jest.mock("@/features/weather/services/fetchWeather")

describe("WeatherSearch Component", () => {

  beforeEach(() => {
    jest.clearAllMocks()
  })

  it("muestra error si el campo de ciudad está vacío", async () => {
    render(<WeatherSearch />)

    await userEvent.click(screen.getByText("Buscar"))

    expect(await screen.findByText("Por favor ingresa el nombre de una ciudad")).toBeInTheDocument()
  })

  it("muestra datos correctamente si la ciudad existe", async () => {
    const mockData = {
      city: "Madrid",
      temperature: 25,
      humidity: 50,
      description: "Soleado"
    }

    // @ts-ignore
    weatherService.fetchWeather.mockResolvedValueOnce(mockData)

    render(<WeatherSearch />)

    const input = screen.getByPlaceholderText("Ingresa el nombre de la ciudad")
    await userEvent.clear(input)
    await userEvent.type(input, "Madrid")

    await userEvent.click(screen.getByText("Buscar"))

    expect(await screen.findByText("Clima en la ciudad de Madrid")).toBeInTheDocument()
    expect(screen.getByText("Temperatura: 25 °C")).toBeInTheDocument()
    expect(screen.getByText("Humedad: 50%")).toBeInTheDocument()
    expect(screen.getByText("Descripción: Soleado")).toBeInTheDocument()
  })

  it("muestra error si la ciudad no existe", async () => {
    // @ts-ignore
    weatherService.fetchWeather.mockRejectedValueOnce(new Error("Ciudad no encontrada"))

    render(<WeatherSearch />)

    const input = screen.getByPlaceholderText("Ingresa el nombre de la ciudad")
    await userEvent.clear(input)
    await userEvent.type(input, "FakeCity")

    await userEvent.click(screen.getByText("Buscar"))

    expect(await screen.findByText("Ciudad no encontrada")).toBeInTheDocument()
  })

  it("muestra error si ocurre un error de red o API", async () => {
    // @ts-ignore
    weatherService.fetchWeather.mockRejectedValueOnce(new Error("Error de red. Verifica tu conexión"))

    render(<WeatherSearch />)

    const input = screen.getByPlaceholderText("Ingresa el nombre de la ciudad")
    await userEvent.clear(input)
    await userEvent.type(input, "Madrid")

    await userEvent.click(screen.getByText("Buscar"))

    expect(await screen.findByText("Error de red. Verifica tu conexión")).toBeInTheDocument()
  })
})