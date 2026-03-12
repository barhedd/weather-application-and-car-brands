import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import WeatherSearch from "@/features/weather/components/WeatherSearch";
import * as weatherService from "@/features/weather/services/fetchWeather";

jest.mock("@/features/weather/services/fetchWeather");

describe("WeatherSearch Component", () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  it("muestra error si el campo de ciudad está vacío", async () => {
    render(<WeatherSearch />);

    const user = userEvent.setup();
    await user.click(screen.getByText("Buscar"));

    expect(await screen.findByText("Por favor ingresa el nombre de una ciudad")).toBeInTheDocument();
  });

  it("muestra datos correctamente si la ciudad existe", async () => {
    const mockData = {
      city: "Madrid",
      temperature: 25,
      humidity: 50,
      description: "Soleado",
    };

    // @ts-ignore
    weatherService.fetchWeather.mockResolvedValueOnce(mockData);

    render(<WeatherSearch />);
    const user = userEvent.setup();

    const input = screen.getByPlaceholderText("Ingresa el nombre de la ciudad");
    await user.clear(input);
    await user.type(input, "Madrid");
    await user.click(screen.getByText("Buscar"));

    expect(await screen.findByText(`Clima en la ciudad de ${mockData.city}`)).toBeInTheDocument();
    expect(screen.getByText(`Temperatura: ${mockData.temperature} °C`)).toBeInTheDocument();
    expect(screen.getByText(`Humedad: ${mockData.humidity}%`)).toBeInTheDocument();
    expect(screen.getByText(`Descripción: ${mockData.description}`)).toBeInTheDocument();
  });

  it("muestra error si la ciudad no existe", async () => {
    // @ts-ignore
    weatherService.fetchWeather.mockRejectedValueOnce(new Error("Ciudad no encontrada"));

    render(<WeatherSearch />);
    const user = userEvent.setup();

    const input = screen.getByPlaceholderText("Ingresa el nombre de la ciudad");
    await user.clear(input);
    await user.type(input, "FakeCity");
    await user.click(screen.getByText("Buscar"));

    expect(await screen.findByText("Ciudad no encontrada")).toBeInTheDocument();
  });

  it("muestra error si ocurre un error de red o API", async () => {
    // @ts-ignore
    weatherService.fetchWeather.mockRejectedValueOnce(new Error("Error de red. Verifica tu conexión"));

    render(<WeatherSearch />);
    const user = userEvent.setup();

    const input = screen.getByPlaceholderText("Ingresa el nombre de la ciudad");
    await user.clear(input);
    await user.type(input, "Madrid");
    await user.click(screen.getByText("Buscar"));

    expect(await screen.findByText("Error de red. Verifica tu conexión")).toBeInTheDocument();
  });
});